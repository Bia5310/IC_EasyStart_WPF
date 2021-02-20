using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace LDZ_Code
{
    public class ImageStyle//инициализация класса сохранения параметров изображения
    {
        public string Extension { set; get; }
        public int Quality { set; get; }
        public string Directory { set; get; }
    }
  

    public static class Files
    {
        public static string OpenDirectory()
        {
            string result = null;
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                result = FBD.SelectedPath;
            }
            if (System.IO.Directory.Exists(result)) return result;
            else return null;
        }
        public static string CreateFilter_ForFileDialog(bool pAllowAllExtensions, params string[] format)
        {
            string result = "";
            for (int i = 0; i < format.Count(); i++)
            {
                if (format[i].IndexOf('.') != -1)
                    format[i] = format[i].Remove(format[i].IndexOf('.'), 1);
                format[i] = String.Format(format[i].ToUpper() + " Files(*.{0}) |*.{0}|", format[i].ToLower());
            }
            for (int i = 0; i < format.Count(); i++)
            {
                result += format[i];
            }
            if (pAllowAllExtensions) result += "All files(*.*) |*.*";
            else { result = result.Substring(0, result.Length - 1); }
            return result;
        }
        public static string OpenSaveDialog(bool AllowAllExtensions, params string[] extensions)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = CreateFilter_ForFileDialog(AllowAllExtensions, extensions);
            SFD.AddExtension = true;
            if (SFD.ShowDialog() == DialogResult.Cancel) return null;
            // получаем выбранный файл
            string filename = SFD.FileName;
            // сохраняем текст в файл
            return filename;
        }
        public static string[] OpenFiles(string Title, bool AllowAllExtensions, bool ismultiselect, params string[] extensions)
        {
            string[] result = null;
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = CreateFilter_ForFileDialog(true, extensions);
            OFD.Title = Title;
            OFD.Multiselect = ismultiselect;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                result = OFD.FileNames;
            }
            return result;
        }
        public static List<string> FindFiles_byExstension(string path, string ext)
        {
            List<string> result = new List<string>();
            if (ext.Substring(0, 1) != ".") return result;

            var allFilenames = Directory.EnumerateFiles(path).Select(p => Path.GetFileName(p));

            // Get all filenames that have a .txt extension, excluding the extension
            var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ext)
                                         .Select(fn => Path.Combine(path, fn));
            result = new List<string>(candidates);
            return result;
        }

        public static List<string> Read_txt(string path)
        {
            string[] AllLines = File.ReadAllLines(path);
            List<string> result = new List<string>(AllLines);
            return result;
        }
        public static bool Write_txt(string path, List<string> data)
        {
            bool result = false;
            try
            {
                string[] AllLines = new string[data.Count];
                data.CopyTo(AllLines);
                File.WriteAllLines(path, AllLines);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static List<int> List_Sort_viaLastNumber(ref List<string> FullImageWays, string ext)
        {
            List<int> result = new List<int>();

            List<string> data_res = new List<string>(FullImageWays.Select(fn => Path.GetFileNameWithoutExtension(fn)));
            result = new List<int>(data_res.Select(fn => Get_LastNumber_from_name(fn)));

            result.Sort(delegate (int wl1, int wl2) { return wl1.CompareTo(wl2); });
            FullImageWays.Sort(delegate (string wl1, string wl2)
            {
                return (Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl1)))
             .CompareTo(Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl2)));
            });
            return result;
        }
        public static int Get_LastNumber_from_name(string a)
        {
            int res = -1;
            res = Convert.ToInt32(a.Substring(a.LastIndexOf('_') + 1));
            return res;
        }
        public static void Get_WLData_byKnownCountofNumbers(int countofnumbers, string[] AllStrings,
            out float[] pWls, out float[] pHzs, out float[] pCoefs)
        {

            float CurWl = 0, CurHz = 0, CurCoef = 0;
            List<float> dWls = new List<float>();
            List<float> dHzs = new List<float>();
            List<float> dCoefs = new List<float>();

            float[] Params = new float[countofnumbers];
            for (int i = 0; i < AllStrings.Length; ++i)
            {
                try
                {
                    Get_WLData_fromDevString(AllStrings[i], countofnumbers, Params);
                    dHzs.Add(Params[0]); dWls.Add(Params[1]); dCoefs.Add(Params[2]);
                }
                catch
                {
                    continue;
                }
            }
            dWls.Reverse();
            dHzs.Reverse();
            dCoefs.Reverse();

            pWls = dWls.ToArray();
            pHzs = dHzs.ToArray();
            pCoefs = dCoefs.ToArray();
        }
        public static void Get_WLData_fromDevString(string datastring, int NumberOfParamsInString, float[] pPars)
        {
            int startindex = 0; bool startfound = false;
            int finishindex = 0; bool finishfound = false;
            List<float> datavalues = new List<float>();
            for (int i = 0; i < datastring.Length; i++)
            {
                if ((datastring[i] != ' ') && ((Char.IsDigit(datastring[i])) || (datastring[i] == '.') || (datastring[i] == ',') || (datastring[i] == '-')))
                {
                    if (startfound)
                    {
                        finishindex++;
                    }
                    else
                    {
                        startindex = i;
                        startfound = true;
                    }
                }
                else
                {
                    if (startfound)
                    {
                        finishindex = i;
                        finishfound = true;
                    }
                }
                if (startfound && finishfound)
                {
                    string data = datastring.Substring(startindex, finishindex - startindex);
                    datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
                    startfound = finishfound = false;
                }
            }
            if (startfound && !finishfound)
            {
                string data = datastring.Substring(startindex);
                datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
            }
            for (int i = 0; i < NumberOfParamsInString; i++) { pPars[i] = datavalues[i]; }
        }
    }

    public class ServiceFunctions
    {
        public static class Files
        {
            public static string OpenDirectory(string Path_was = null)
            {
                string result = null;
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    result = FBD.SelectedPath;
                }
                else
                { result = Path_was; }
                if (System.IO.Directory.Exists(result))
                {
                    try
                    {

                    }
                    catch
                    {

                    }
                    return result;
                }
                else return null;
            }
            public static string CreateFilter_ForFileDialog(bool pAllowAllExtensions, params string[] format)
            {
                string result = "";
                for (int i = 0; i < format.Count(); i++)
                {
                    if (format[i].IndexOf('.') != -1)
                        format[i] = format[i].Remove(format[i].IndexOf('.'), 1);
                    format[i] = String.Format(format[i].ToUpper() + " Files(*.{0}) |*.{0}|", format[i].ToLower());
                }
                for (int i = 0; i < format.Count(); i++)
                {
                    result += format[i];
                }
                if (pAllowAllExtensions) result += "All files(*.*) |*.*";
                else { result = result.Substring(0, result.Length - 1); }
                return result;
            }
            public static string OpenSaveDialog(bool AllowAllExtensions, params string[] extensions)
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = CreateFilter_ForFileDialog(AllowAllExtensions, extensions);
                SFD.AddExtension = true;
                if (SFD.ShowDialog() == DialogResult.Cancel) return null;
                // получаем выбранный файл
                string filename = SFD.FileName;
                // сохраняем текст в файл
                return filename;
            }
            public static string[] OpenFiles()
            {
                string[] result = null;
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Filter = "Spectrum TXT Files(*.txt)|*.txt|All Files|*.*";
                OFD.Title = "Select Spectrum Files";
                OFD.Multiselect = true;

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    result = OFD.FileNames;
                }
                return result;
            }
            public static List<string> FindFiles_byExstension(string path, string ext)
            {
                List<string> result = new List<string>();
                if (ext.Substring(0, 1) != ".") return result;

                var allFilenames = Directory.EnumerateFiles(path).Select(p => Path.GetFileName(p));

                // Get all filenames that have a .txt extension, excluding the extension
                var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ext)
                                             .Select(fn => Path.Combine(path, fn));
                result = new List<string>(candidates);
                return result;
            }

            public static List<string> Read_txt(string path)
            {
                string[] AllLines = File.ReadAllLines(path);
                List<string> result = new List<string>(AllLines);
                return result;
            }
            public static bool Write_txt(string path, List<string> data)
            {
                bool result = false;
                try
                {
                    string[] AllLines = new string[data.Count];
                    data.CopyTo(AllLines);
                    File.WriteAllLines(path, AllLines);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                return result;
            }
            public static List<int> List_Sort_viaLastNumber(ref List<string> FullImageWays, string ext)
            {
                List<int> result = new List<int>();

                List<string> data_res = new List<string>(FullImageWays.Select(fn => Path.GetFileNameWithoutExtension(fn)));
                result = new List<int>(data_res.Select(fn => Get_LastNumber_from_name(fn)));

                result.Sort(delegate (int wl1, int wl2) { return wl1.CompareTo(wl2); });
                FullImageWays.Sort(delegate (string wl1, string wl2)
                {
                    return (Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl1)))
                 .CompareTo(Get_LastNumber_from_name(Path.GetFileNameWithoutExtension(wl2)));
                });
                return result;
            }
            public static int Get_LastNumber_from_name(string a)
            {
                int res = -1;
                res = Convert.ToInt32(a.Substring(a.LastIndexOf('_') + 1));
                return res;
            }
            public static void Get_WLData_byKnownCountofNumbers(int countofnumbers, string[] AllStrings,
                out float[] pWls, out float[] pHzs, out float[] pCoefs)
            {

                float CurWl = 0, CurHz = 0, CurCoef = 0;
                List<float> dWls = new List<float>();
                List<float> dHzs = new List<float>();
                List<float> dCoefs = new List<float>();

                float[] Params = new float[countofnumbers];
                for (int i = 0; i < AllStrings.Length; ++i)
                {
                    try
                    {
                        Get_WLData_fromDevString(AllStrings[i], countofnumbers, Params);
                        dHzs.Add(Params[0]); dWls.Add(Params[1]); dCoefs.Add(Params[2]);
                    }
                    catch
                    {
                        continue;
                    }
                }
                dWls.Reverse();
                dHzs.Reverse();
                dCoefs.Reverse();

                pWls = dWls.ToArray();
                pHzs = dHzs.ToArray();
                pCoefs = dCoefs.ToArray();
            }
            public static void Get_WLData_fromDevString(string datastring, int NumberOfParamsInString, float[] pPars)
            {
                int startindex = 0; bool startfound = false;
                int finishindex = 0; bool finishfound = false;
                List<float> datavalues = new List<float>();

                //Оказывается, не во всех системах все читается корректно. Делаем проверку
                bool isDotNeeded = false;
                double datastr = 0;
                try { datastr = Convert.ToDouble(("0.1155").Replace('.', ',')); isDotNeeded = false; }
                catch { isDotNeeded = true; }
                try { datastr = Convert.ToDouble(("0,1155").Replace(',', '.')); isDotNeeded = true; }
                catch { isDotNeeded = false; }


                for (int i = 0; i < datastring.Length; i++)
                {
                    if ((datastring[i] != ' ') && ((Char.IsDigit(datastring[i])) || (datastring[i] == '.') || (datastring[i] == ',') || (datastring[i] == '-')))
                    {
                        if (startfound)
                        {
                            finishindex++;
                        }
                        else
                        {
                            startindex = i;
                            startfound = true;
                        }
                    }
                    else
                    {
                        if (startfound)
                        {
                            finishindex = i;
                            finishfound = true;
                        }
                    }
                    if (startfound && finishfound)
                    {
                        string data = datastring.Substring(startindex, finishindex - startindex);


                        datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
                        startfound = finishfound = false;
                    }
                }
                if (startfound && !finishfound)
                {
                    string data = datastring.Substring(startindex);
                    if (isDotNeeded)
                        datavalues.Add((float)Convert.ToDouble(data.Replace(',', '.')));
                    else
                        datavalues.Add((float)Convert.ToDouble(data.Replace('.', ',')));
                }
                for (int i = 0; i < NumberOfParamsInString; i++) { pPars[i] = datavalues[i]; }
            }
        }
        public static class Math
        {
            public static class EmguCV
            {
                /*  public static double CalculateSpecSum(Matrix<float> matrix)
                  {
                      int cx = matrix.Cols / 2;
                      int cy = matrix.Rows / 2;
                      double sum = 0;
                      int sqradius = (int)(1f * cy);
                      int startposX = matrix.Cols / 2 - sqradius;
                      int finishposX = matrix.Cols / 2 + sqradius;
                      int startposY = matrix.Rows / 2 - sqradius;
                      int finishposY = matrix.Rows / 2 + sqradius;
                      for (int i = startposY; i < finishposY; i++)
                      {
                          for (int j = startposX; j < finishposX; j++)
                          {
                              sum += Math.Abs(matrix[i, j] * (Math.Sqrt(Math.Pow(i - cy, 2.0) + Math.Pow(j - cx, 2.0))));
                          }
                      }
                      return sum;
                  }
                  public static void SwitchQuadrants(ref Matrix<float> matrix)
                  {
                      int cx = matrix.Cols / 2;
                      int cy = matrix.Rows / 2;

                      Matrix<float> q0 = matrix.GetSubRect(new Rectangle(0, 0, cx, cy));
                      Matrix<float> q1 = matrix.GetSubRect(new Rectangle(cx, 0, cx, cy));
                      Matrix<float> q2 = matrix.GetSubRect(new Rectangle(0, cy, cx, cy));
                      Matrix<float> q3 = matrix.GetSubRect(new Rectangle(cx, cy, cx, cy));
                      Matrix<float> tmp = new Matrix<float>(q0.Size);

                      q0.CopyTo(tmp);
                      q3.CopyTo(q0);
                      tmp.CopyTo(q3);
                      q1.CopyTo(tmp);
                      q2.CopyTo(q1);
                      tmp.CopyTo(q2);
                  }
                  public static Matrix<float> GetDftMagnitude(Matrix<float> fftData)
                  {
                      //MassiveForChanels
                      Matrix<float>[] outMain = new Matrix<float>[2];
                      //The Real part of the Fourier Transform
                      Matrix<float> outReal = new Matrix<float>(fftData.Size);
                      //The imaginary part of the Fourier Transform
                      Matrix<float> outIm = new Matrix<float>(fftData.Size);

                      var outr = fftData.Split();
                      outReal = outr[0];
                      outIm = outr[1];
                      float summ = 0;
                      for (int i = 0; i < outIm.Rows; i++)
                      {
                          for (int j = 0; j < outIm.Cols; j++)
                          {
                              summ += outIm[i, j];
                          }
                      }
                      CvInvoke.Pow(outReal, 2.0, outReal);
                      CvInvoke.Pow(outIm, 2.0, outIm);

                      CvInvoke.Add(outReal, outIm, outReal);
                      CvInvoke.Pow(outReal, 0.5, outReal);

                      int cols = outReal.Cols;
                      int rows = outReal.Rows;
                      Matrix<float> OnesMatrix = new Matrix<float>(rows, cols, 1);
                      OnesMatrix.SetValue(1);
                      // CvInvoke.Add(outReal, OnesMatrix, outReal); // 1 + Mag
                      //  CvInvoke.Log(outReal, outReal); // log(1 + Mag)            

                      return outReal;
                  }
                  public static Matrix<float> GetDftMagnitude(UMat fftData)
                  {
                      //MassiveForChanels
                      Matrix<float> fftData2 = new Matrix<float>(fftData.Rows, fftData.Cols);
                      fftData.CopyTo(fftData2);
                      Matrix<float>[] outMain = new Matrix<float>[2];
                      //The Real part of the Fourier Transform
                      Matrix<float> outReal = new Matrix<float>(fftData.Size);
                      //The imaginary part of the Fourier Transform
                      Matrix<float> outIm = new Matrix<float>(fftData.Size);

                      var outr = fftData2.Split();
                      outReal = outr[0];
                      outIm = outr[1];
                      float summ = 0;
                      for (int i = 0; i < outIm.Rows; i++)
                      {
                          for (int j = 0; j < outIm.Cols; j++)
                          {
                              summ += outIm[i, j];
                          }
                      }
                      CvInvoke.Pow(outReal, 2.0, outReal);
                      CvInvoke.Pow(outIm, 2.0, outIm);

                      CvInvoke.Add(outReal, outIm, outReal);
                      CvInvoke.Pow(outReal, 0.5, outReal);

                      int cols = outReal.Cols;
                      int rows = outReal.Rows;
                      Matrix<float> OnesMatrix = new Matrix<float>(rows, cols, 1);
                      OnesMatrix.SetValue(1);
                      CvInvoke.Add(outReal, OnesMatrix, outReal); // 1 + Mag
                      CvInvoke.Log(outReal, outReal); // log(1 + Mag)            

                      return outReal;
                  }
                  public static Bitmap Matrix2Bitmap(Matrix<float> matrix)
                  {
                      CvInvoke.Normalize(matrix, matrix, 0.0, 255.0, Emgu.CV.CvEnum.NormType.MinMax);

                      Image<Gray, float> image = new Image<Gray, float>(matrix.Size);
                      matrix.CopyTo(image);

                      return image.ToBitmap();
                  }*/
            }
            public static List<PointF> Interpolate_curv(float[] Wls, float[] Hzs)
            {
                List<PointF> result = new List<PointF>();
                int count_of_gaps = Wls.Count() - 1;

                for (int i = 0; i < count_of_gaps; i++)
                {
                    result.Add(new PointF(Wls[i], Hzs[i]));
                    float count_of_wls_to_restruct = (Wls[i + 1] - Wls[i] - 1);
                    for (int j = 1; j <= count_of_wls_to_restruct; j++)
                    {
                        float new_hz_val = Hzs[i] + j * ((float)(Hzs[i + 1] - Hzs[i])) / (Wls[i + 1] - Wls[i]);// = (x- Hzs[i] )/ (float)j;
                        result.Add(new PointF(Wls[i] + j, new_hz_val));
                    }
                }
                result.Add(new PointF(Wls[count_of_gaps], Hzs[count_of_gaps]));
                return result;
            }
            public static void Interpolate_curv(ref float[] Wls, ref float[] Hzs)
            {
                List<PointF> result = new List<PointF>();
                int count_of_gaps = Wls.Count() - 1;

                for (int i = 0; i < count_of_gaps; i++)
                {
                    result.Add(new PointF(Wls[i], Hzs[i]));
                    float count_of_wls_to_restruct = (Wls[i + 1] - Wls[i] - 1);
                    for (int j = 1; j <= count_of_wls_to_restruct; j++)
                    {
                        float new_hz_val = Hzs[i] + j * ((float)(Hzs[i + 1] - Hzs[i])) / (Wls[i + 1] - Wls[i]);// = (x- Hzs[i] )/ (float)j;
                        result.Add(new PointF(Wls[i] + j, new_hz_val));
                    }
                }
                result.Add(new PointF(Wls[count_of_gaps], Hzs[count_of_gaps]));
                int data_count = result.Count();
                Wls = new float[data_count];
                Hzs = new float[data_count];
                for (int i = 0; i < data_count; i++)
                {
                    Wls[i] = result[i].X;
                    Hzs[i] = result[i].Y;
                }
            }
            public static double Interpolate_value(double x1, double y1, double x2, double y2, double x_tofind)
            {
                return ((y2 - y1) / (x2 - x1)) * (x_tofind - x1) + y1;
            }
            public static List<PointF> Curv_normalize(List<PointF> InitCurv, float NormValue)
            {
                List<PointF> result = new List<PointF>();
                float MaxValue = 0;
                for (int i = 0; i < InitCurv.Count; i++)
                {
                    if (MaxValue < InitCurv[i].Y) MaxValue = InitCurv[i].Y;
                }
                for (int i = 0; i < InitCurv.Count; i++)
                {
                    result.Add(new PointF(InitCurv[i].X, (InitCurv[i].Y * NormValue / MaxValue)));
                }
                return result;
            }
            public static double PerfectRounding(double val, int CharN)
            {
                double mnoj = (float)System.Math.Pow(10.0, CharN);
                val *= mnoj;
                if (System.Math.Abs(val - (int)val) > 0.5f) return (((int)val + 1 * System.Math.Sign(val - (int)val)) / mnoj);
                else return ((int)val / mnoj);
            }
        }
        public static class Controls
        {
            public static class ZGraph
            {
                /*  public static void Spectrum_Add(ZedGraph.ZedGraphControl pZGraph, List<PointF> pSpectrum, string name, Color color, SymbolType sType,
                      System.Drawing.Drawing2D.DashStyle LineStyle, bool Initial_Visability)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;

                      // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
                      //  pane.CurveList.Clear();

                      // Создадим список точек
                      PointPairList list = new PointPairList();

                      // Заполняем список точек
                      int Count_ofPts = pSpectrum.Count();
                      for (int i = 0; i < Count_ofPts; i++)
                      {
                          // добавим в список точку
                          list.Add(pSpectrum[i].X, pSpectrum[i].Y);
                      }

                      // Создадим кривую с названием "Sinc", 
                      // которая будет рисоваться голубым цветом (Color.Blue),
                      // Опорные точки выделяться не будут (SymbolType.None)
                      LineItem myCurve = pane.AddCurve(name, list, color, sType);
                      myCurve.Line.Style = LineStyle;
                      myCurve.IsVisible = Initial_Visability;
                      // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                      // В противном случае на рисунке будет показана только часть графика, 
                      // которая умещается в интервалы по осям, установленные по умолчанию
                      pZGraph.AxisChange();

                      // Обновляем график
                      pZGraph.Invalidate();
                  }
                  public static void Spectrum_Replace(ZedGraph.ZedGraphControl pZGraph, List<PointF> pSpectrum, string name, Color color, SymbolType sType,
                      System.Drawing.Drawing2D.DashStyle LineStyle, bool Initial_Visability,int Position)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;

                      // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
                      //  pane.CurveList.Clear();

                      // Создадим список точек
                      PointPairList list = new PointPairList();

                      // Заполняем список точек
                      int Count_ofPts = pSpectrum.Count();
                      for (int i = 0; i < Count_ofPts; i++)
                      {
                          // добавим в список точку
                          list.Add(pSpectrum[i].X, pSpectrum[i].Y);
                      }

                      // Создадим кривую с названием "Sinc", 
                      // которая будет рисоваться голубым цветом (Color.Blue),
                      // Опорные точки выделяться не будут (SymbolType.None)
                      LineItem myCurve = pane.AddCurve(name, list, color, sType);
                      myCurve.Line.Style = LineStyle;
                      myCurve.IsVisible = Initial_Visability;
                      pane.CurveList[Position] = pane.CurveList[pane.CurveList.Count - 1];
                      pane.CurveList.RemoveAt(pane.CurveList.Count - 1);
                      // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
                      // В противном случае на рисунке будет показана только часть графика, 
                      // которая умещается в интервалы по осям, установленные по умолчанию
                      pZGraph.AxisChange();

                      // Обновляем график
                      pZGraph.Invalidate();
                  }
                  public static void Change_Labels(ZedGraph.ZedGraphControl pZGraph, string Title, string xLabel, string yLabel)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      pane.IsFontsScaled = false;

                      pane.Title = Title;
                      pane.XAxis.Title = xLabel;
                      pane.YAxis.Title = yLabel;
                      pZGraph.AxisChange();
                      pZGraph.Invalidate();
                  }
                  public static void Change_X_Limits(ZedGraph.ZedGraphControl pZGraph, double XMin, double XMax)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      // pane.XAxis.MinAuto = false;
                      pane.XAxis.Min = XMin;
                      //pane.XAxis.MaxAuto = false;
                      pane.XAxis.Max = XMax;
                      pZGraph.AxisChange();
                      pZGraph.Invalidate();

                  }
                  public static void Change_AutoScale(ZedGraph.ZedGraphControl pZGraph, string XY, bool state)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      if (XY == "X")
                      {
                          pane.XAxis.MinAuto = state;
                          pane.XAxis.MaxAuto = state;
                      }
                      else if (XY == "Y")
                      {
                          pane.YAxis.MinAuto = state;
                          pane.YAxis.MaxAuto = state;
                      }
                      else //"XY" or another control parameter
                      {
                          pane.XAxis.MinAuto = state;
                          pane.XAxis.MaxAuto = state;
                          pane.YAxis.MinAuto = state;
                          pane.YAxis.MaxAuto = state;
                      }
                      pZGraph.AxisChange();
                      pZGraph.Invalidate();
                  }
                  public static void Change_GraphColor(ZedGraph.ZedGraphControl pZGraph, int Graph_ind, Color FinalColor)
                  {
                      GraphPane pane = pZGraph.GraphPane;
                      pane.CurveList[Graph_ind].Color = FinalColor;
                      pZGraph.Invalidate();

                  }
                  public static void Spectrum_Delete(ZedGraph.ZedGraphControl pZGraph, int index)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;
                      pane.CurveList.RemoveAt(index);
                      pZGraph.Invalidate();

                  }
                  public static void Spectrum_ChangeVisability(ZedGraph.ZedGraphControl pZGraph, int index, bool IsVisible)
                  {
                      // Получим панель для рисования
                      GraphPane pane = pZGraph.GraphPane;
                      pane.CurveList[index].IsVisible = IsVisible;
                      pZGraph.Invalidate();
                  }
                  public static void Spectrum_AddInvisibleBorders(ZedGraph.ZedGraphControl pZGraph, double b1, double b2)
                  {
                      GraphPane pane = pZGraph.GraphPane;

                      LineItem line1 = new LineItem(String.Empty, new[] { b1, b1 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line1.Line.Width = 1f;
                      pane.CurveList.Add(line1);
                      pane.CurveList[pane.CurveList.Count - 1].IsVisible = false;

                      LineItem line2 = new LineItem(String.Empty, new[] { b2, b2 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line2.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line2.Line.Width = 1f;
                      pane.CurveList.Add(line2);
                      pane.CurveList[pane.CurveList.Count - 1].IsVisible = false;

                      pZGraph.Invalidate();
                  }
                  public static void Spectrum_reDrawBorders(ZedGraph.ZedGraphControl pZGraph, double b1, double b2, bool IsVisible)
                  {
                      GraphPane pane = pZGraph.GraphPane;

                      LineItem line1 = new LineItem(String.Empty, new[] { b1, b1 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line1.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line1.Line.Width = 1f;
                      pane.CurveList.RemoveAt(pane.CurveList.Count - 2);
                      pane.CurveList.Add(line1);

                      LineItem line2 = new LineItem(String.Empty, new[] { b2, b2 },
                      new[] { pane.YAxis.Min, pane.YAxis.Max },
                      Color.Black, SymbolType.None);
                      line2.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                      line2.Line.Width = 1f;
                      pane.CurveList.RemoveAt(pane.CurveList.Count - 2);
                      pane.CurveList.Add(line2);

                      pane.CurveList[pane.CurveList.Count - 2].IsVisible = IsVisible;
                      pane.CurveList[pane.CurveList.Count - 1].IsVisible = IsVisible;

                      pZGraph.Invalidate();
                  }*/
            }
            public static class XLSx
            {
                /* public static void WriteColomn_ofValues(ref IXLWorksheet xls_sheet,string Description,
                     List<float> Values,int pRow,int pColomn)
                 {
                     xls_sheet.Cell(pRow, pColomn).Value = Description;

                     for(int i =0;i<Values.Count;i++)
                     {
                         xls_sheet.Cell(pRow + i + 1, pColomn).Value = Values[i];
                     }
                 }
                 public static void WriteColomn_ofValues(ref IXLWorksheet xls_sheet, string Description,
                    List<int> Values, int pRow, int pColomn)
                 {
                     xls_sheet.Cell(pRow, pColomn).Value = Description;

                     for (int i = 0; i < Values.Count; i++)
                     {
                         xls_sheet.Cell(pRow + i + 1, pColomn ).Value = Values[i];
                     }
                 }
                 public static void WriteColomn_ofStrings(ref IXLWorksheet xls_sheet,
                     List<string> Strings, int pRow, int pColomn)
                 {
                     for (int i = 0; i < Strings.Count; i++)
                     {
                         xls_sheet.Cell(pRow+i , pColomn).Value = Strings[i];
                     }
                 }
                 public static void WriteColomn_ofFormalas_R1C1(ref IXLWorksheet xls_sheet, string Description,
                  List<string> Strings, int pRow, int pColomn)
                 {
                     xls_sheet.Cell(pRow, pColomn).Value = Description;
                     for (int i = 0; i < Strings.Count; i++)
                     {
                         xls_sheet.Cell(pRow +1 + i, pColomn).FormulaR1C1 = Strings[i];
                     }
                 }*/
            }
            public static void ResizeControl_ForImage(ref Panel Pan_IMG, ref PictureBox PB_CurrentImage, Bitmap WorkingImage, out PointF Transformation)
            {
                var RealImSize = WorkingImage.Size;
                double ImW2H = ((double)RealImSize.Width) / ((double)RealImSize.Height);
                double ContW2H = ((double)Pan_IMG.Width) / ((double)Pan_IMG.Height);
                Size NewPBSize = new Size(0, 0);
                Point NewPosition = new Point(0, 0);
                if (ContW2H > ImW2H) //то вписываем по высоте
                {
                    NewPBSize.Height = Pan_IMG.Height;
                    NewPBSize.Width = (int)((double)Pan_IMG.Height * ImW2H);
                    //   PB_CurrentImage.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                    NewPosition = new Point((Pan_IMG.Width - NewPBSize.Width) / 2, 0);
                }
                else //вписываем по ширине
                {
                    NewPBSize.Width = Pan_IMG.Width;
                    NewPBSize.Height = (int)((double)Pan_IMG.Width / ImW2H);

                    //  PB_CurrentImage.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
                    NewPosition = new Point(0, (Pan_IMG.Height - NewPBSize.Height) / 2);
                }
                PB_CurrentImage.Size = NewPBSize;
                PB_CurrentImage.Location = NewPosition;

                Transformation = new PointF((float)RealImSize.Width / (float)NewPBSize.Width, (float)RealImSize.Height / (float)NewPBSize.Height);
            }
            public static void Init_TrB(TrackBar pTrB, int min, int max, int value)
            {
                pTrB.Minimum = min;
                pTrB.Maximum = max;
                pTrB.Value = value;
            }
            public static void Init_NUD(NumericUpDown NUD, int min, int max, int value)
            {
                NUD.Minimum = min;
                NUD.Maximum = max;
                NUD.Value = value;
            }
            public delegate void InvokeDelegate_forTrackBar(ref TrackBar ppTrackBar, int a);
            public static void SetValue_onTrackBar_Manually(ref TrackBar pTrackBar, int pValue)
            {
                if (pTrackBar.InvokeRequired)
                {
                    InvokeDelegate_forTrackBar del1 = new InvokeDelegate_forTrackBar(SetValue_onTrackBar_Manually);
                    pTrackBar.BeginInvoke(del1, pTrackBar, pValue);
                }
                else
                {
                    pTrackBar.Value = pValue;
                }
            }
        }
        public static class AO
        {
            public static List<int> GetWLS_fromList_plusSort(ref List<string> FullImageWays, string ext)
            {
                List<int> result = new List<int>();
                if (FullImageWays.Find(x => x.Contains("color")) != null)
                    FullImageWays.RemoveAt(FullImageWays.IndexOf(FullImageWays.Find(x => x.Contains("color"))));
                List<string> data_res = new List<string>(FullImageWays.Select(fn => Path.GetFileNameWithoutExtension(fn)));
                result = new List<int>(data_res.Select(fn => Get_WL_from_string(fn)));
                result.Sort(delegate (int wl1, int wl2) { return wl1.CompareTo(wl2); });
                FullImageWays.Sort(delegate (string wl1, string wl2)
                {
                    return (Get_WL_from_string(Path.GetFileNameWithoutExtension(wl1)))
                 .CompareTo(Get_WL_from_string(Path.GetFileNameWithoutExtension(wl2)));
                });
                return result;
            }
            public static int Get_WL_from_string(string a)
            {
                int res = -1;
                res = Convert.ToInt32(a.Substring(a.LastIndexOf('_') + 1));
                return res;
            }
            public static void Pack_AO_Parameters(ref List<object> pParamList,
                                 bool IsEmulator, bool TurnedON, bool handling_byslider,
                                 string DEV_required, string DEV_loaded,
                                 float pStartL, float pEndL, float pStepL, float pCurrentL, float pMaxL, float pMinL,
                                 double SW_FreqDev, double SW_TimeDev, double SW_FreqDevMax, bool SW_on,
                                 float[] pWls_2pack, float[] pHZs_2pack, float[] pCoefs_2pack)
            {
                pParamList.Clear();

                pParamList.Add(IsEmulator);
                pParamList.Add(TurnedON);
                pParamList.Add(handling_byslider);

                pParamList.Add(DEV_required);
                pParamList.Add(DEV_loaded);

                pParamList.Add(pStartL);
                pParamList.Add(pEndL);
                pParamList.Add(pStepL);
                pParamList.Add(pCurrentL);
                pParamList.Add(pMaxL);
                pParamList.Add(pMinL);

                pParamList.Add(SW_FreqDev);
                pParamList.Add(SW_TimeDev);
                pParamList.Add(SW_FreqDevMax);
                pParamList.Add(SW_on);

                pParamList.Add(pWls_2pack);
                pParamList.Add(pHZs_2pack);
                pParamList.Add(pCoefs_2pack);
            }
            public static void UnPack_AO_Parameters(List<object> pParamList,
                                       ref bool IsEmulator, ref bool TurnedON, ref bool handling_byslider,
                                       ref string DEV_required, ref string DEV_loaded,
                                       ref float pStartL, ref float pEndL, ref float pStepL, ref float pCurrentL, ref float pMaxL, ref float pMinL,
                                       ref double Frequency_deviation, ref double Time_of_deviation, ref double Frequency_dev_Max, ref bool IsSweepOn,
                                       ref float[] pWls, ref float[] pHZs, ref float[] pCoefs)
            {

                IsEmulator = (bool)pParamList[0];
                TurnedON = (bool)pParamList[1];
                handling_byslider = (bool)pParamList[2];

                DEV_required = pParamList[3] as string;
                DEV_loaded = pParamList[4] as string;

                pStartL = (float)pParamList[5];
                pEndL = (float)pParamList[6];
                pStepL = (float)pParamList[7];
                pCurrentL = (float)pParamList[8];
                pMaxL = (float)pParamList[9];
                pMinL = (float)pParamList[10];
                Frequency_deviation = (double)pParamList[11];
                Time_of_deviation = (double)pParamList[12];
                Frequency_dev_Max = (double)pParamList[13];
                IsSweepOn = (bool)pParamList[14];

                pWls = pParamList[15] as float[];
                pHZs = pParamList[16] as float[];
                pCoefs = pParamList[17] as float[];
            }
            public delegate void StringDelegate(string parameter);
            public static bool ConnectAOF(string DevName, ref TrackBar pTrB_WlCont, ref NumericUpDown pTB_CurWl, bool isSimulator,
                dynamic D_mess, dynamic D_err,
                ref float pMinL, ref float pMaxL, ref float pStartL, ref float pFinishL, ref float pCurrentL, ref string pAO_reqName)
            {
                bool result = false;
                var LogError_del = D_err;
                var LogMessage_del = D_mess;

                int codeerr = 0; int num = 0;
                try
                {
                    /* int devs_to_open = LDZ_Code.AO.ListUnopenDevices();
                     if (devs_to_open > 0)
                     {
                         LDZ_Code.AO.List_and_open_Devices();
                         result = true;
                     }
                     else
                     {
                         result = false;
                         LogError_del("Подключенные АОФ не найдены");
                     }*/
                }
                catch (Exception exc)
                {
                    result = false;
                    LogError_del("Произошла ошибка при инициализации АОФ");
                    LogMessage_del("Оригинал ошибки: " + exc.Message);
                }

                try
                {
                    pStartL = pMinL;
                    pFinishL = pMaxL;
                    pCurrentL = (pMinL + pMaxL) / 2;
                    Controls.Init_TrB(pTrB_WlCont,
                        (int)(pTB_CurWl.Minimum = (int)(pMinL)),
                        (int)(pTB_CurWl.Maximum = (int)(pMaxL)),
                        (int)(pTB_CurWl.Value = (int)(pCurrentL)));
                }
                catch (Exception ex)
                {
                    LogError_del(ex.Message);
                }
                return result;
            }
            public static void PowerAOF(bool isSimulator, dynamic D_mess, dynamic D_err, ref bool isPowerOn)
            {
                /* var LogError_del = D_err;
                 var LogMessage_del = D_mess;
                 try
                 {
                     int codeerr = 0;
                     codeerr = LDZ_Code.AO.AOM_PowerOn(isSimulator);
                     if (codeerr != 0)
                     {
                         isPowerOn = false;
                         throw new Exception(LDZ_Code.AO.AOM_IntErr(codeerr));
                     }
                     else
                     {
                         LogMessage_del("AOF Power is ON");
                         isPowerOn = true;
                     }
                 }
                 catch (Exception ex)
                 {
                     LogError_del(ex.Message);
                 }*/
            }
        }
        public static class UI
        {
            public static string GetDateString()
            {
                string res = DateTime.Today.ToString();
                return ((res.Substring(0, res.IndexOf(' '))).Remove(res.IndexOf('.'), 1)).Remove(res.LastIndexOf('.') - 1, 1);
            }
            public static string Get_TimeNow_String()
            {
                return DateTime.Now.ToString().Replace('.', '_').Replace(' ', '_').Replace(':', '_');
            }
            public static class Log
            {
                public class Logger
                {

                    public delegate void Log_del(string message);
                    int AttachmentFactor = 1;
                    ListBox ControlledLB;
                    public Logger(ListBox pLBConsole)
                    {
                        ControlledLB = pLBConsole;
                        Log.CreateAttachmentFactor(ref AttachmentFactor, ControlledLB);
                    }
                    public void Message(string Message)
                    {
                        Log.Message(ControlledLB, AttachmentFactor, Message);
                    }
                    public void Error(string Message)
                    {
                        Log.Error(ControlledLB, AttachmentFactor, Message);
                    }

                }
                public class FileLogger
                {
                    string FileName = "";
                    List<string> Doings = new List<string>();
                    public FileLogger(string fFile)
                    {
                        FileName = fFile;
                        try { Log("Logger Initialized"); }
                        catch { }

                    }
                    public void Log(string Info)
                    {
                        try
                        { Doings.Add(UI.Get_TimeNow_String() + ": " + Info);
                        Files.Write_txt(FileName, Doings);
                        }
                        catch { }
                    }
                }
                /// <summary>
                /// Сообщает об ошибке в элемент ListBox, используемый как коноль вывода
                /// </summary>
                /// <param name="message">The message</param>
                private static void Message(ListBox pLBConsole, int pAttachmentFactor, string message)
                {
                    if (null == message)
                    {
                        throw new ArgumentNullException("message");
                    }
                    string data = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}: {1}", DateTime.Now, message);
                    object index;
                    if (data.Length <= pAttachmentFactor)
                    {
                        index = data;
                        if (pLBConsole.InvokeRequired)
                            pLBConsole.BeginInvoke((Action)(() => Message(pLBConsole, pAttachmentFactor, message)));
                        else
                            pLBConsole.Items.Insert(0, index);
                    }
                    else
                    {
                        index = data.Substring(0, (int)pAttachmentFactor) + "...";
                        if (pLBConsole.InvokeRequired)
                        {
                            pLBConsole.BeginInvoke((Action)(() => Message(pLBConsole, pAttachmentFactor, message)));
                            pLBConsole.BeginInvoke((Action)(() => Attachment(pLBConsole, pAttachmentFactor, data.Substring((int)pAttachmentFactor), 1)));
                        }
                        else
                        {
                            pLBConsole.Items.Insert(0, index);
                            Log.Attachment(pLBConsole, pAttachmentFactor, data.Substring((int)pAttachmentFactor), 1);
                        }
                    }

                }

                /// <summary>
                /// Add an error log message and show an error message box
                /// </summary>
                /// <param name="message">The message</param>
                private static void Error(ListBox LBConsole, int pAttachmentFactor, string message)
                {
                    Log.Message(LBConsole, pAttachmentFactor, "Ошибка: " + message);
                }
                private static void Attachment(ListBox pLBConsole, int pAttachmentFactor, string Addmessage, int level)
                {
                    if (null == Addmessage)
                    {
                        throw new ArgumentNullException("message");
                    }
                    string data = Addmessage;
                    object index;
                    if (data.Length <= pAttachmentFactor)
                    {
                        index = "..." + data;
                        pLBConsole.Items.Insert(level, index);
                    }
                    else
                    {
                        index = "..." + data.Substring(0, (int)pAttachmentFactor) + "...";
                        pLBConsole.Items.Insert(level, index);
                        Log.Attachment(pLBConsole, pAttachmentFactor, data.Substring((int)pAttachmentFactor), level + 1);
                    }

                }
                private static void CreateAttachmentFactor(ref int pAttachmentFactor, ListBox pLB)
                {
                    const float widthofthesymbol = 5.8f;
                    pAttachmentFactor = (int)(((float)pLB.Width) / widthofthesymbol) - 1;
                }

            }
        }
    }
}
        
