using Assistance;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volte.Data.Dapper;
using Volte.Data.Json;
using Volte.Data.SqlKata;
using Volte.Utils;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WinFormsApp1
{
    public partial class frmTools : Form
    {
        private string g_AccessToken = "";
        private string baseUrl = "";

        public frmTools()
        {
            InitializeComponent();
        }
        private string AdminLink()
        {
            baseUrl = Environment.Text;
            string getAccessToken = "/api/openapi/v1/auth/getAccessToken";
            Console.WriteLine("appKey");
            Console.Write("->:");
            Console.WriteLine("appSecurity");
            Console.Write("->:");
            JSONObject jSONObject = new JSONObject();
            jSONObject.SetValue("appKey", sAppId.Text);
            jSONObject.SetValue("appSecurity", sAppSecret.Text);
            string result = new PostHttpHelper(baseUrl, getAccessToken, jSONObject.ToString()).GetResult();


            JSONObject jSONObject2 = new JSONObject(result);
            if (!string.IsNullOrEmpty(result) && result.Contains("accessToken"))
            {
                g_AccessToken = jSONObject2.Attr("value.accessToken");
                corporationId.Text = jSONObject2.Attr("value.corporationId");
                textBox1.Text = g_AccessToken;
                string text6 = "?accessToken=" + g_AccessToken + "&staffBy=id&pathType=name";
                string adminPath = "/api/openapi/v1/roledefs/$" + Util.UrlEncode(corporationId.Text) + ":admin" + text6;
                string staffId = "";
                var adminRole = new GetHttpHelper(baseUrl, adminPath).GetResult();

                foreach (JSONObject jSONObject5 in new JSONObject(adminRole).GetJSONObject("value").GetJSONArray("contents").JSONObjects)
                {
                    foreach (string name in jSONObject5.GetJSONArray("staffs").Names)
                    {
                        staffId = name;
                    }
                }
                if (string.IsNullOrEmpty(staffId))
                {
                    staffId = UserId.Text;
                }

                adminPath = "/api/openapi/v1.1/provisional/getProvisionalAuth?accessToken=" + g_AccessToken;
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                dictionary2["accesstoken"] = accessToken.Text;
                jSONObject2 = new JSONObject();
                jSONObject2.SetValue("uid", staffId);
                jSONObject2.SetValue("pageType", "home");
                jSONObject2.SetValue("isApplet", false);
                jSONObject2.SetValue("expireDate", "604800");
                result = new PostHttpHelper(baseUrl, adminPath, jSONObject2.ToString(), dictionary2).GetResult();
                var jSONObject3 = new JSONObject(result);

                string vv = jSONObject3.Attr("value.message");
                if (!string.IsNullOrEmpty(vv) && vv.Length > 10)
                {
                    int p1 = vv.IndexOf("accessToken=");
                    if (p1 < 0)
                    {
                        lbl_msg.ForeColor = Color.Red;
                        lbl_msg.Text = vv;
                        lbl_msg.Visible = true;

                        timer1.Stop();
                        timer1.Interval = 10000;
                        timer1.Start();
                    }
                    else
                    {
                        int p2 = vv.IndexOf("&ekbCorpId", p1);
                        if (p1 > 0 && p2 > 0 && p2 > p1)
                        {
                            accessToken.Text = vv.Substring(p1 + 12, p2 - p1 - 12);
                        }
                        SetEnable(true);

                        adminPath = "api/v1/organization/corporations/info?sensitiveWords=true&corpId=" + Util.UrlEncode(corporationId.Text) + "&accessToken=" + accessToken.Text;
                        var corpo = new GetHttpHelper(baseUrl, adminPath).GetResult();
                        var corpSONObject = new JSONObject(corpo);
                        corpName.Text = corpSONObject.GetJSONObject("value").GetValue("name");

                        return vv;
                    }
                }

            }
            else
            {
                lbl_msg.ForeColor = Color.Red;
                lbl_msg.Text = jSONObject2.Attr("value.errorMessage");
                lbl_msg.Visible = true;

                timer1.Stop();
                timer1.Interval = 10000;
                timer1.Start();
            }
            return "";
        }

        private void Fetch_Click(object sender, EventArgs e)
        {
            FetchUrl(false, true);
        }

        private void FetchUrl(bool autoOpen, bool copy)
        {
            lbl_msg.ForeColor = Color.Green;
            lbl_msg.Text = "正在处理中...";
            lbl_msg.Visible = true;
            this.Parse();
            string link = AdminLink();
            if (!string.IsNullOrEmpty(link))
            {
                SetEnable(true);


                if (copy)
                {
                    Clipboard.SetText(link);
                    lbl_msg.ForeColor = Color.Green;
                    lbl_msg.Text = "✔已经复制到粘贴板";
                    lbl_msg.Visible = true;
                }

                timer1.Stop();
                timer1.Interval = 10000;
                timer1.Start();
                string fileName = "Assistance.json";
                if (File.Exists(fileName))
                {
                    string setting = "{}";
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        setting = sr.ReadToEnd();
                    }
                    JSONObject jSONObject = new JSONObject(setting);
                    JSONObject item = jSONObject.GetJSONObject(corporationId.Text);
                    item.SetValue("Url", Environment.Text);
                    item.SetValue("Key", sAppId.Text);
                    item.SetValue("Security", sAppSecret.Text);
                    item.SetValue("Name", corpName.Text);
                    item.SetValue("User", UserId.Text);

                    jSONObject.SetValue(corporationId.Text, item);
                    Util.WriteContents(fileName, jSONObject.ToString(), false);
                }
                if (autoOpen)
                {
                    try
                    {
                        OpenBrowser(link);
                    }
                    catch (Exception ex)
                    {
                        lbl_msg.Text = ("无法打开浏览器: " + ex.Message);
                    }
                }
            }
        }
        private void OpenBrowser(string url)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void sAppId_TextChanged(object sender, EventArgs e)
        {
            UserAccess.Enabled = false;
        }
        private void Parse()
        {
            if (sAppId.Text.Contains("{") && sAppId.Text.Contains("}") && sAppId.Text.Contains("\""))
            {
                JSONObject t = new JSONObject(sAppId.Text);

                sAppId.Text = t.GetValue("appKey");
                sAppSecret.Text = t.GetValue("appSecurity");
            }

            if (sAppId.Text.Contains("\r") || sAppId.Text.Contains("\n"))
            {
                string t = sAppId.Text.Replace("\r", "\n");
                t = t.Replace("\t", "");
                t = t.Replace("\n\n", "\n");
                t = t.Trim(new char[] { '\n' });
                var a = t.Split(new char[] { '\n' });
                if (a.Length == 2)
                {
                    sAppId.Text = a[0];
                    sAppSecret.Text = a[1];
                }
                else if (a.Length == 3 && string.IsNullOrEmpty(a[0]))
                {
                    sAppId.Text = a[1];
                    sAppSecret.Text = a[2];
                }
            }
        }
        private int states = 0;
        private Keys prechar = Keys.A;
        private void sAppId_KeyUp(object sender, KeyEventArgs e)
        {

            if (prechar == Keys.K && !(e.KeyCode == Keys.L || e.KeyCode == Keys.M))
            {
                states = 0;
            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                this.Parse();

            }
            else if (e.KeyCode == Keys.K && e.Control && states == 0)
            {
                states = 1;
            }
            else if (e.Control && states == 1)
            {
                states = 0;
                if (e.KeyCode == Keys.L)
                {
                    string fileName = "Assistance.json";
                    if (File.Exists(fileName))
                    {
                        string setting = "{}";
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            setting = sr.ReadToEnd();
                        }

                        JSONObject jSONObject = new JSONObject(setting);
                        if (jSONObject.ContainsKey("Hash"))
                        {
                            JSONObject v = jSONObject.GetJSONObject("Hash");
                            if (!string.IsNullOrEmpty(v.GetValue("Security")))
                            {
                                TxtHash.Text = v.GetValue("Security");
                            }
                        }
                    }
                    this.Size = new Size(this.Size.Width, TxtBase64.Location.Y + TxtBase64.Size.Height * 2);
                }
                else if (e.KeyCode == Keys.M)
                {
                    this.Size = new Size(this.Size.Width, TxtHash.Location.Y + 40);

                }
            }
            prechar = e.KeyCode;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sAppId_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FetchUserUrl(false, true);
        }
        private void FetchUserUrl(bool autoOpen, bool copy)
        {
            if (string.IsNullOrEmpty(UserId.Text))
            {
                lbl_msg.ForeColor = Color.Red;
                lbl_msg.Text = "请输入授权用户名";
                lbl_msg.Visible = true;
                return;
            }
            lbl_msg.ForeColor = Color.Green;
            lbl_msg.Text = "正在处理中...";
            lbl_msg.Visible = true;

            baseUrl = Environment.Text;
            string v = UserId.Text;
            v = v.Replace("，", ",");
            v = v.Replace("；", ",");
            v = v.Replace(";", ",");
            string vUserId = "";
            string vUserName = UserId.Text;
            if (v.Contains(","))
            {
                var aUserId = v.Split(',');
                vUserId = aUserId[1];
                vUserName = aUserId[0];
            }
            JSONObject t = new JSONObject();
            t.SetValue("filterBy", "((name.contains(\"" + vUserName + "\")))");

            string queryUserId = "/api/v2/organization/staffs/list?corpId=" + Util.UrlEncode(corporationId.Text);
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
            dictionary2["accesstoken"] = accessToken.Text;
            JSONObject jSONObject2 = new JSONObject();
            jSONObject2.SetValue("query", t);

            var tt = jSONObject2.ToString();
            var result = new PostHttpHelper(baseUrl, queryUserId, jSONObject2.ToString(), dictionary2).GetResult();
            var jSONObject3 = new JSONObject(result);
            if (jSONObject3.GetInteger("count") > 0)
            {
                JSONObject item = jSONObject3.GetJSONArray("items").JSONObject(0);

                string staffId = item.GetValue("id");
                bool found = false;
                if (!string.IsNullOrEmpty(vUserId))
                {
                    foreach (JSONObject it in jSONObject3.GetJSONArray("items").JSONObjects)
                    {
                        if (it.GetValue("id").Contains(vUserId))
                        {
                            staffId = it.GetValue("id");
                            found = true;
                        }
                    }
                }

                if (!found && !string.IsNullOrEmpty(vUserId))
                {
                    foreach (JSONObject it in jSONObject3.GetJSONArray("items").JSONObjects)
                    {
                        if (it.GetValue("code") == vUserId)
                        {
                            staffId = it.GetValue("id");
                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    foreach (JSONObject it in jSONObject3.GetJSONArray("items").JSONObjects)
                    {
                        if (it.GetValue("name") == UserId.Text)
                        {
                            staffId = it.GetValue("id");
                            found = true;
                        }
                    }
                }
                string adminPath = "/api/openapi/v1.1/provisional/getProvisionalAuth?accessToken=" + g_AccessToken;
                dictionary2 = new Dictionary<string, string>();
                jSONObject2 = new JSONObject();
                jSONObject2.SetValue("uid", staffId);
                jSONObject2.SetValue("pageType", "home");
                jSONObject2.SetValue("isApplet", false);
                jSONObject2.SetValue("expireDate", "604800");
                result = new PostHttpHelper(baseUrl, adminPath, jSONObject2.ToString(), dictionary2).GetResult();
                var jSONObject4 = new JSONObject(result);

                string vv = jSONObject4.Attr("value.message");

                if (copy)
                {
                    lbl_msg.ForeColor = Color.Green;
                    lbl_msg.Text = "✔已经复制到粘贴板";
                    lbl_msg.Visible = true;
                    Clipboard.SetText(vv);
                }
                SetEnable(true);
                if (autoOpen)
                {
                    try
                    {
                        OpenBrowser(vv);
                    }
                    catch (Exception ex)
                    {
                        lbl_msg.Text = ("无法打开浏览器: " + ex.Message);
                    }
                }
            }
            else
            {
                lbl_msg.ForeColor = Color.Red;
                lbl_msg.Text = "查无【" + UserId.Text + "】";
                lbl_msg.Visible = true;
            }
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
        }

        private void corporationId_TextChanged(object sender, EventArgs e)
        {
            //corporationId_ReadKey();
        }
        private void corporationId_ReadKey()
        {
            string fileName = "Assistance.json";
            if (File.Exists(fileName))
            {
                string setting = "{}";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    setting = sr.ReadToEnd();
                }

                JSONObject jSONObject = new JSONObject(setting);
                if (jSONObject.ContainsKey(corporationId.Text))
                {
                    JSONObject v = jSONObject.GetJSONObject(corporationId.Text);
                    sAppId.Text = v.GetValue("Key");
                    sAppSecret.Text = v.GetValue("Security");
                    corpName.Text = v.GetValue("Name");
                    UserId.Text = v.GetValue("User");

                    if (!string.IsNullOrEmpty(v.GetValue("Url")))
                    {
                        Environment.Text = v.GetValue("Url");
                    }
                    SetEnable(false);
                    lbl_msg.ForeColor = Color.Green;
                    lbl_msg.Text = "已经读取新的配置";
                    lbl_msg.Visible = true;
                }
                else
                {
                    List<string> strings = new List<string>();
                    foreach (string k in jSONObject.Names)
                    {
                        JSONObject v = jSONObject.GetJSONObject(k);
                        if (v.GetValue("Name").Contains(corporationId.Text) && !string.IsNullOrEmpty(corporationId.Text))
                        {
                            strings.Add(k);
                        }
                    }
                    if (strings.Count == 1)
                    {
                        corporationId.Text = strings[0];
                        JSONObject v = jSONObject.GetJSONObject(corporationId.Text);
                        corpName.Text = v.GetValue("Name");
                    }
                    else
                    {
                        lbl_msg.ForeColor = Color.Red;
                        lbl_msg.Text = "找不到配置";
                        lbl_msg.Visible = true;
                    }
                }
                timer1.Stop();
                timer1.Interval = 10000;
                timer1.Start();

            }
        }
        private void SetEnable(bool Enabled)
        {
            UserOpen.Enabled = Enabled;
            UserAccess.Enabled = Enabled;
        }
        private void corporationId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                corporationId_ReadKey();
            }
        }

        private void UserId_TextChanged(object sender, EventArgs e)
        {
            UserAccess.Text = UserId.Text + "-链接";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (TxtHash.Text.Length > 30)
            {
                TxtBase64.Text = DapperUtil.Compress(TxtSQL.Text, TxtHash.Text);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtHash_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtHash.Text) && TxtHash.Text.Length > 30)
            {
                string fileName = "Assistance.json";
                if (File.Exists(fileName))
                {
                    string setting = "{}";
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        setting = sr.ReadToEnd();
                    }
                    JSONObject item = new JSONObject();
                    item.SetValue("Key", "Hash");
                    item.SetValue("Security", TxtHash.Text);

                    JSONObject jSONObject = new JSONObject(setting);
                    jSONObject.SetValue("Hash", item);
                    Util.WriteContents(fileName, jSONObject.ToString(), false);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_msg.Visible = false;
            lbl_msg.ForeColor = Color.Green;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FetchUrl(true, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FetchUserUrl(true, false);
        }

        private void corporationId_KeyDown(object sender, KeyPressEventArgs e)
        {

        }

        private void sAppSecret_TextChanged(object sender, EventArgs e)
        {

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
                lbl_msg.ForeColor = Color.Red;
                lbl_msg.Text = "AI(" + useAIComment + ")";
                lbl_msg.Visible = true;

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
        public bool useAIComment = false;
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
            if (isRunning || !useAIComment)
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
        private void label4_Click(object sender, EventArgs e)
        {
            lbl_msg.ForeColor = Color.Red;
            lbl_msg.Text = "Running:" + isRunning + ";AI:" + useAIComment;
            lbl_msg.Visible = true;
            timer1.Stop();
            timer1.Interval = 10000;
            timer1.Start();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, e) =>
            {
                fetchIssues("");
            };
            worker.RunWorkerAsync();
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
                useAIComment = jSONObject.GetBoolean("AI_COMMENT");
                Assignees = jSONObject.GetValue("Assignees");
            }
            WriteCache("useAIComment_" + useAIComment);
        }
    }
}
