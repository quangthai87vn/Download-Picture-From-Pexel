using System.Collections.Concurrent;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DownloadApp
{
    public partial class Form1 : Form
    {
        // TODO: thay bằng API Key của bạn
        private const string PEXELS_API_KEY = "zoHthZo7MfjWKicfVt6ZyuaqB6kXKomkGEQlrIas34ACXgbc26MITlgx";

        private readonly PexelsClient _client;
        private readonly HttpClient _http = new HttpClient();
        private CancellationTokenSource? _cts;

        public Form1()
        {
            InitializeComponent();
            _client = new PexelsClient(PEXELS_API_KEY);

            // defaults
            txtQuery.Text = "bánh trung thu";
            numLimit.Value = 30;
            txtSaveDir.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                "PexelsDownload");

            btnSearch.Click += async (_, __) => await DoSearchAsync();
            btnDownload.Click += async (_, __) => await DoDownloadSelectedAsync();
            btnPickDir.Click += (_, __) => PickDir();
            chkSelectAll.CheckedChanged += (_, __) => ToggleSelectAll(chkSelectAll.Checked);
            advsearch.Click += async (_, __) => await Advsearch_Click();
            // optional cancel
            // btnCancel.Click += (_, __) => _cts?.Cancel();
        }

        private void ToggleSelectAll(bool isChecked)
        {
            foreach (Control c in flow.Controls)
            {
                if (c is Panel p)
                {
                    var cb = p.Controls.OfType<CheckBox>().FirstOrDefault();
                    if (cb != null)
                        cb.Checked = isChecked;
                }
            }
        }

        private void PickDir()
        {
            using var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtSaveDir.Text;
            if (fbd.ShowDialog(this) == DialogResult.OK)
                txtSaveDir.Text = fbd.SelectedPath;
        }

        private async Task DoSearchAsync()
        {
            var query = txtQuery.Text.Trim();
            var perPage = (int)numLimit.Value;
            if (string.IsNullOrWhiteSpace(query)) { MessageBox.Show("Nhập từ khóa!"); return; }

            ToggleUi(false);
            flow.Controls.Clear();
            lblStatus.Text = "Đang tìm ảnh...";
            progressBar.Value = 0;

            try
            {
                
                // Pexels cho per_page tối đa 80
                var result = await _client.SearchPhotosAsync(query,pageSize: perPage);//, page: 1, perPage: perPage);

                if (result?.photos == null || result.photos.Count == 0)
                {
                    lblStatus.Text = "Không thấy ảnh nào.";
                    return;
                }

                // render thumb + checkbox
                foreach (var p in result.photos)
                {
                    flow.Controls.Add(MakePhotoCard(p));
                }
                lblStatus.Text = $"Tìm thấy {result.photos.Count} ảnh (hiển thị {result.photos.Count}). Tick để tải.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search error");
            }
            finally
            {
                ToggleUi(true);
            }
        }
        private Control MakePhotoCard(Photo p)
        {
            // Panel chứa thumbnail + checkbox + label
            var panel = new Panel
            {
                Width = 220,
                Height = 220,
                Margin = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = p // lưu object Photo để dùng lại
            };

            var pb = new PictureBox
            {
                Dock = DockStyle.Top,
                Height = 160,
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand
            };
            // dùng ảnh nhỏ để nhanh UI
            pb.LoadAsync(p.source.small);

            // label info
            var lbl = new Label
            {
                Dock = DockStyle.Top,
                Height = 32,
                Text = $"ID {p.id}\n{p.photographer}",
                AutoEllipsis = true
            };

            var cb = new CheckBox
            {
                Dock = DockStyle.Bottom,
                Text = "Chọn tải",
                Height = 24
            };

            // click ảnh để preview lớn trên trình duyệt
            pb.Click += (_, __) => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = p.url,
                UseShellExecute = true
            });

            panel.Controls.Add(cb);
            panel.Controls.Add(lbl);
            panel.Controls.Add(pb);
            return panel;
        }
        private void btnPickDir_Click(object sender, EventArgs e)
        {

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        //private async Task DownloadByIdsAsync(IEnumerable<long> ids, string saveDir, CancellationToken ct = default)
        //{
        //    Directory.CreateDirectory(saveDir);
        //    progressBar.Value = 0;
        //    progressBar.Maximum = ids.Count();

        //    var sem = new SemaphoreSlim(4);
        //    var tasks = ids.Select(async id =>
        //    {
        //        await sem.WaitAsync(ct);
        //        try
        //        {
        //            var photo = await _client.GetPhotoAsync(id);
        //            var url = photo.src.original ?? photo.src.large2x ?? photo.src.large;
        //            var ext = Path.GetExtension(new Uri(url).AbsolutePath);
        //            if (string.IsNullOrEmpty(ext)) ext = ".jpg";
        //            var path = Path.Combine(saveDir, $"{photo.id}{ext}");

        //            await DownloadFileAsync(url, path, ct);

        //            this.Invoke(() =>
        //            {
        //                progressBar.Value += 1;
        //                lblStatus.Text = $"Đã tải {progressBar.Value}/{progressBar.Maximum}";
        //            });
        //        }
        //        finally { sem.Release(); }
        //    });

        //    await Task.WhenAll(tasks);
        //}




        // ========== DOWNLOAD ==========
        private async Task DoDownloadSelectedAsync()
        {
            var saveDir = txtSaveDir.Text.Trim();
            if (string.IsNullOrWhiteSpace(saveDir))
            {
                MessageBox.Show("Chọn thư mục lưu!");
                return;
            }
            Directory.CreateDirectory(saveDir);

            // lấy các Photo đã tick
            var selected = new List<Photo>();
            foreach (Control c in flow.Controls)
            {
                if (c is Panel p && p.Tag is Photo photo)
                {
                    var cb = p.Controls.OfType<CheckBox>().FirstOrDefault();
                    if (cb?.Checked == true) selected.Add(photo);
                }
            }
            if (selected.Count == 0)
            {
                MessageBox.Show("Chưa chọn ảnh nào!");
                return;
            }

            ToggleUi(false);
            progressBar.Value = 0;
            progressBar.Maximum = selected.Count;
            lblStatus.Text = "Đang tải ảnh...";

            _cts = new CancellationTokenSource();

            try
            {
                // tải song song (tối đa 4 luồng để tránh nghẽn I/O)
                var sem = new SemaphoreSlim(4);
                var tasks = selected.Select(async photo =>
                {
                    await sem.WaitAsync(_cts.Token);
                    try
                    {
                        // ưu tiên link chất lượng cao. Có thể đổi sang photo.src.large / original
                        var url = photo.source.original ?? photo.source.large2x ?? photo.source.large ?? photo.source.medium;
                        var ext = Path.GetExtension(new Uri(url).AbsolutePath);
                        if (string.IsNullOrEmpty(ext)) ext = ".jpg";
                        var safeName = $"{photo.id}{ext}";
                        var path = Path.Combine(saveDir, safeName);

                        await DownloadFileAsync(url, path, _cts.Token);

                        this.Invoke(() =>
                        {
                            progressBar.Value += 1;
                            lblStatus.Text = $"Đã tải {progressBar.Value}/{progressBar.Maximum}";
                        });
                    }
                    finally
                    {
                        sem.Release();
                    }
                }).ToList();

                await Task.WhenAll(tasks);
                lblStatus.Text = $"✅ Hoàn tất. Đã lưu vào: {saveDir}";
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Đã hủy.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download error");
            }
            finally
            {
                ToggleUi(true);
            }
        }

        private async Task DownloadFileAsync(string url, string destPath, CancellationToken ct)
        {
            using var resp = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct);
            resp.EnsureSuccessStatusCode();

            await using var httpStream = await resp.Content.ReadAsStreamAsync(ct);
            await using var fs = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true);
            await httpStream.CopyToAsync(fs, 81920, ct);
        }

        private void ToggleUi(bool enabled)
        {
            btnSearch.Enabled = enabled;
            btnDownload.Enabled = enabled;
            btnPickDir.Enabled = enabled;
            // btnCancel.Enabled = !enabled;
        }



        //1) Gọi API có filter(orientation / color / locale…)
        //SDK PexelsDotNetSDK hỗ trợ các tham số phổ biến như page, perPage, orientation, color, size, locale(tùy phiên bản). Dùng được thì truyền; không được thì bỏ bớt.
        private async Task<List<Photo>> SearchPhotosPagedAsync(
                string query,
                int totalWanted = 100,
                string? orientation = null,   // "landscape" | "portrait" | "square"
                string? color = null,         // "red", "orange", "green", hex "FFA500"...
                string? size = null,          // "large" | "medium" | "small" (nếu SDK hỗ trợ)
                string? locale = "en-US"      // thử "en-US"; nếu SDK có "vi-VN" thì test thêm
            )
        {
            var photos = new List<Photo>();
            int perPage = Math.Min(80, totalWanted);
            int page = 1;

            while (photos.Count < totalWanted)
            {
                var res = await _client.SearchPhotosAsync(
                    query: query,
                    page: page,
                    
                   // perPage: perPage,
                    color: color,
                    orientation: orientation,
                    size: size,
                    locale: locale
                );

                if (res?.photos == null || res.photos.Count == 0) break;

                photos.AddRange(res.photos);
                if (res.photos.Count < perPage) break; // hết trang
                page++;
            }

            // cắt đúng số lượng cần
            return photos.Take(totalWanted).ToList();
        }

        //2) Rerank + lọc theo “include/exclude keywords”
        //Tự tăng độ “chuẩn nghĩa” bằng cách chấm điểm theo alt, url, photographer…

        private static int RelevanceScore(Photo p, IEnumerable<string> include, IEnumerable<string> exclude)
        {
            var text = $"{p.alt} {p.url} {p.photographer}".ToLowerInvariant();

            int score = 0;
            foreach (var k in include)
                if (!string.IsNullOrWhiteSpace(k) && text.Contains(k.ToLowerInvariant()))
                    score += 2;

            foreach (var k in exclude)
                if (!string.IsNullOrWhiteSpace(k) && text.Contains(k.ToLowerInvariant()))
                    score -= 3;

            // bonus theo orientation/ratio nếu cần
            return score;
        }

        private List<Photo> FilterAndRank(
            IEnumerable<Photo> photos,
            IEnumerable<string> include,
            IEnumerable<string> exclude,
            int minWidth = 0, int minHeight = 0)
        {
            return photos
                .Where(p => (minWidth == 0 || p.width >= minWidth) &&
                            (minHeight == 0 || p.height >= minHeight))
                .OrderByDescending(p => RelevanceScore(p, include, exclude))
                .ToList();
        }

        //3) Hợp nhất nhiều truy vấn(query expansion)
        //Gom ảnh từ nhiều từ khóa, rồi hợp nhất + rerank:
        private async Task<List<Photo>> SearchMultiAsync(params string[] queries)
        {
            var bag = new ConcurrentBag<Photo>();
            await Task.WhenAll(queries.Select(async q =>
            {
                var list = await SearchPhotosPagedAsync(q, totalWanted: 80, locale: "en-US");
                foreach (var p in list) bag.Add(p);
            }));

            // loại trùng theo id
            var unique = bag.GroupBy(p => p.id).Select(g => g.First()).ToList();
            return unique;
        }

        private void RenderPhotosToFlowLayout(IEnumerable<Photo> photos)
        {
            flow.Controls.Clear();

            foreach (var p in photos)
            {
                // Panel chứa 1 ảnh
                var panel = new Panel
                {
                    Width = 220,
                    Height = 220,
                    Margin = new Padding(8),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = p
                };

                var pb = new PictureBox
                {
                    Dock = DockStyle.Top,
                    Height = 160,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Cursor = Cursors.Hand
                };

                // Load ảnh thumbnail để UI nhanh
                pb.LoadAsync(p.source.medium ?? p.source.small ?? p.source.tiny);

                var lbl = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 32,
                    Text = $"{p.photographer}",
                    AutoEllipsis = true
                };

                var cb = new CheckBox
                {
                    Dock = DockStyle.Bottom,
                    Text = "Chọn tải",
                    Height = 24
                };

                pb.Click += (_, __) => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = p.url,
                    UseShellExecute = true
                });

                panel.Controls.Add(cb);
                panel.Controls.Add(lbl);
                panel.Controls.Add(pb);

                flow.Controls.Add(panel);
            }

            lblStatus.Text = $"Hiển thị {photos.Count()} ảnh sau lọc.";
        }

        private async Task Advsearch_Click()
        {
            var include = new[] { "bánh canh", "bánh" };
            var exclude = new[] { "snow", "christmas", "pancake" }; // loại nhiễu hay gặp

            var raw = await SearchPhotosPagedAsync(
                query: "bánh canh",
                totalWanted: 120,
                orientation: "square",     // ví dụ: muốn hình vuông
                color: null,
                size: null,
                locale: "vn-VN"
            );

            var ranked = FilterAndRank(raw, include, exclude, minWidth: 1500, minHeight: 1500);

            RenderPhotosToFlowLayout(ranked);
        }




    }
}
