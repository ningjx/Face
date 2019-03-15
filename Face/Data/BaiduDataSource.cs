using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.Data
{
    //class BaiduDataSource
    //{
        public class User_list
        {
            /// <summary>
            /// 用户组
            /// </summary>
            public string group_id { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public string user_id { get; set; }
            /// <summary>
            /// 用户信息
            /// </summary>
            public string user_info { get; set; }
            /// <summary>
            /// 匹配得分
            /// </summary>
            public double score { get; set; }
        }

        public class MatchRoot
        {
            /// <summary>
            /// 人脸唯一标识
            /// </summary>
            public string face_token { get; set; }
            /// <summary>
            /// 用户信息表
            /// </summary>
            public List<User_list> user_list { get; set; }
        }

    //}
}
