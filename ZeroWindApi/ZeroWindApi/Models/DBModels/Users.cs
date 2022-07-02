namespace ZeroWindApi.Models.DBModels
{
    public class Users
    {
        //编号
        public string? Id { get; set; }

        //站主昵称
        public string? NickName { get; set; }

        //图片路径
        public string? Img { get; set; }

        //出生日期
        public long Birthday { get; set; }

        //个人简介
        public string? Intro { get; set; }
    }
}
