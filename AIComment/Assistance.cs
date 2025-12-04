using Assistance;
using System.ComponentModel;
using Volte.Data.Json;
using Volte.Utils;

namespace WinFormsApp1
{
    public partial class frmTools : Form
    {

        public frmTools()
        {
            InitializeComponent();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();  // 退出应用程序
        }

        private void frmTools_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;  // 取消关闭窗体
                this.Hide();  // 隐藏窗体
                this.notifyIcon1.Visible = true;  // 显示托盘图标
            }
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();  // 显示窗体
            this.WindowState = FormWindowState.Normal;  // 恢复窗体正常大小
            this.notifyIcon1.Visible = true;  // 隐藏托盘图标
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();  // 退出应用程序
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (sender, e) =>
                {
                    fetchIssues("");
                    Thread.Sleep(5000); // 模拟长时间运行的任务
                };
                worker.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("请求错误: " + ex.Message);
            }
        }
        public bool isRunning = false;
        public string Assignees = "";
        public bool load = false;
        public Dictionary<string, bool> cache = new();

        private async void WriteCache(string key)
        {
            cache[key] = true;

            string fileName = "cache.json";
            string setting = "{}";

            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    setting = sr.ReadToEnd();
                }
            }
            JSONObject jSONObject = new JSONObject(setting);
            jSONObject.SetValue(key, true);
            Util.WriteContents(fileName, jSONObject.ToString(), false);
        }

        private void ReadCache()
        {
            if (load)
            {
                return;
            }
            load = true;
            string fileName = "cache.json";
            if (File.Exists(fileName))
            {
                string setting = "{}";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    setting = sr.ReadToEnd();
                }
                JSONObject jSONObject = new JSONObject(setting);
                foreach (string k in jSONObject.Names)
                {
                    cache[k] = true;
                }
            }
        }

        private async void fetchIssues(string all)
        {
            ReadCache();
            if (isRunning || !checkBox1.Checked)
            {
                return;
            }
            try
            {

                isRunning = true;
                int startAt = 0;
                int totalIssues = 0;
                int MAX_RESULTS = 100;
                do
                {
                    string JQL_QUERY = "jql=project = 产研项目 AND updated>" + DateTime.Now.ToString("yyyy-MM-dd") + " ORDER BY updated desc";
                    if ("all" == all)
                    {
                        JQL_QUERY = "jql=project = 产研项目 ORDER BY updated desc";
                    }
                    string url = string.Format("/rest/api/2/search?" + JQL_QUERY + "&startAt=" + startAt + "&maxResults=" + MAX_RESULTS);
                    var responseBody = new NewGetHttpHelper("https://jira.hosecloud.com", url, "Basic eWp5OlN1bnJpc2UxMjM0NTY3OA==").GetResult();

                    try
                    {
                        JSONObject json = new JSONObject(responseBody);
                        if (totalIssues == 0)
                        {
                            totalIssues = json.GetInteger("total");
                            Console.WriteLine("共发现 " + totalIssues + " 个issue");
                        }
                        JSONArray issues = json.GetJSONArray("issues");
                        processIssues(issues);
                        startAt += MAX_RESULTS;
                        //Thread.sleep(1000);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } while (startAt < totalIssues);
            }
            catch (Exception ex)
            {
                Console.WriteLine("请求错误: " + ex.Message);
            }
            finally
            {
                isRunning = false;
            }
        }

        private string AskAI(string query)
        {
            string url = "https://dify.ekuaibao.net";

            JSONObject obj = new JSONObject();
            obj.SetValue("inputs", new JSONObject());
            obj.SetValue("response_mode", "blocking");
            obj.SetValue("query", query);
            obj.SetValue("conversation_id", "");
            obj.SetValue("user", "yjy");

            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2["Authorization"] = "Bearer app-mFuo8yDVgpjV2GeSEgDsKgCE";
            var result = new PostHttpHelper(url, "/v1/chat-messages", obj.ToString(), dictionary2).GetResult();
            return result;
        }
        private const string AI_COMMENT_FLAG = "AI-Comment";

        private string AI_COMMENT_TAG = "(*y)_The following is an " + AI_COMMENT_FLAG + "_\n\r_以下为AI评论_\n\r===============================\n";
        public string AddAIComment(string key, string comment)
        {
            string url = "/rest/api/2/issue/" + key + "/comment";

            JSONObject obj = new JSONObject();
            obj.SetValue("body", AI_COMMENT_TAG + comment);

            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2["Authorization"] = "Basic eWp5OlN1bnJpc2UxMjM0NTY3OA==";
            var result = new PostHttpHelper("https://jira.hosecloud.com", url, obj.ToString(), dictionary2).GetResult();
            WriteCache(key);
            return result;
        }

        private void processIssues(JSONArray issues)
        {
            var _assignees = Assignees.Split(',');
            foreach (var issue in issues.JSONObjects)
            {
                string id = issue.GetValue("id");
                string key = issue.GetValue("key");
                string summary = issue.GetJSONObject("fields").GetValue("summary");
                string description = issue.GetJSONObject("fields").GetValue("description");
                string assignee = issue.GetJSONObject("fields").Attr("assignee.displayName");

                if (!cache.ContainsKey(key))
                {
                    string url = "/rest/api/2/issue/" + key + "?expand=changelog";
                    var responseBody = new NewGetHttpHelper("https://jira.hosecloud.com", url, "Basic eWp5OlN1bnJpc2UxMjM0NTY3OA==").GetResult();
                    bool bHasAIComment = false;
                    JSONObject json = new JSONObject(responseBody);

                    JSONObject fields = json.GetJSONObject("fields");
                    JSONObject comment = fields.GetJSONObject("comment");

                    JSONArray comments = comment.GetJSONArray("comments");

                    foreach (var tComment in comments.JSONObjects)
                    {
                        var body = tComment.GetValue("body");
                        if (!string.IsNullOrEmpty(body) && body.Contains(AI_COMMENT_FLAG))
                        {
                            bHasAIComment = true;
                        }
                    }
                    JSONObject changelog = json.GetJSONObject("changelog");
                    if (changelog != null)
                    {
                        JSONArray histories = changelog.GetJSONArray("histories");

                        foreach (var history in histories.JSONObjects)
                        {
                            JSONArray changeItem = history.GetJSONArray("items");
                            foreach (var tcommm in changeItem.JSONObjects)
                            {
                                string field = tcommm.GetValue("field");
                                string from = tcommm.GetValue("from");
                                if (field == "Comment" && !string.IsNullOrEmpty(from) && from.Contains(AI_COMMENT_FLAG))
                                {
                                    bHasAIComment = true;
                                }
                            }
                        }
                        if (!bHasAIComment && !cache.ContainsKey(key) && (_assignees.Contains(assignee)))
                        {
                            string sAIComment = AskAI(description);
                            JSONObject ai = new JSONObject(sAIComment);
                            string answer = ai.GetValue("answer");
                            if (!string.IsNullOrEmpty(answer))
                            {
                                AddAIComment(key, answer);
                            }
                        }
                    }
                }
            }
        }

        private void frmTools_Load(object sender, EventArgs e)
        {
            string fileName = "setting.json";
            if (File.Exists(fileName))
            {
                string setting = "{}";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    setting = sr.ReadToEnd();
                }

                JSONObject jSONObject = new JSONObject(setting);
                Assignees = jSONObject.GetValue("Assignees");
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, e) =>
            {
                fetchIssues("");
            };
            worker.RunWorkerAsync();

        }

        private void Exist_Click(object sender, EventArgs e)
        {
            Application.Exit();  // 退出应用程序
        }
    }
}
