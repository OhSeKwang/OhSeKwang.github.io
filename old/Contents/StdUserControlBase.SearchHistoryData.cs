using HHI.NexFrame.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HHI.NexFrame.Client.UI.UserControls
{
    partial class StdUserControlBase
    {
        /// <summary>
        /// 기본 경로를 이용 한 검색 정보 로드
        /// </summary>
        /// <returns></returns>
        protected SearchForm GetSearchHistoryData()
        {
            // 기본 지원 파일명을 가져 온다.
            return GetSearchHistoryData(GetSearchHistoryDataFileName());
        }

        /// <summary>
        /// 요청한 경로의 파일을 찾아서 검색 정보 로드 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected SearchForm GetSearchHistoryData(string fileName)
        {
            if (File.Exists(fileName) == false) return null;

            SearchHistoryData searchData = null;
            XmlSerializer ser = new XmlSerializer(typeof(SearchHistoryData));
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                searchData = (SearchHistoryData)ser.Deserialize(reader);
            }
            SearchForm searchForm = searchData.FormList.Where(a => a.Name == this.GetType().FullName).FirstOrDefault();

            return searchForm;
        }

        /// <summary>
        /// 파일명 반환
        /// </summary>
        /// <returns></returns>
        private string GetSearchHistoryDataFileName()
        {
            string strFileName = this.GetSearchHistoryDataFileName();
            strFileName = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), strFileName);

            return strFileName;
        }

        /// <summary>
        /// 기본 경로를 이용 한 검색 정보 저장
        /// </summary>
        /// <param name="searchHistory"></param>
        protected void SaveSearchHistoryData(SearchHistoryData searchHistory)
        {
            this.SaveSearchHistoryData(searchHistory, this.GetSearchHistoryDataFileName());
        }

        /// <summary>
        /// 검색 정보 저장
        /// </summary>
        /// <param name="searchHistory"></param>
        /// <param name="saveFileName"></param>
        protected void SaveSearchHistoryData(SearchHistoryData searchHistory, string saveFileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(SearchHistoryData));
            using (TextWriter writer = new StreamWriter(saveFileName))
            {
                ser.Serialize(writer, searchHistory);
                writer.Close();
            }
        }

    }
}
