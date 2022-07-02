using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ZeroWindApi.Models.DBModels
{
    public class Blogs
    {
		//博客编号
		public string? Id { get; set; }
		//标题
		public string? Title { get; set; }
		//内容
		public string? Context { get; set; }
		//发布时间
		public long CreateTime { get; set; }
		//更新时间
		public long UpdateTime { get; set; }
		//分类Id
		public int TypeId { get; set; }
		//分类名称
		public string? Type { get; set; }
		//标签
		public string? Tags { get; set; }
	}

	public class BlogsModel
	{
		public BlogsModel() { }

		public BlogsModel(Blogs blogs)
        {
			Id = blogs.Id;
			Title = blogs.Title;
			Context = blogs.Context;
			CreateTime = blogs.CreateTime;
			UpdateTime = blogs.UpdateTime;
			TypeId = blogs.TypeId;
			Type = blogs.Type;
			Tags = blogs.Tags!.Split(',');
        }

		//博客编号
		public string? Id { get; set; }
		
		[Required(ErrorMessage = "博客标题不得为空")]
		//标题
		public string? Title { get; set; }

		[MinLength(500,ErrorMessage = "博客内容必须大于500字")]
		//内容
		public string? Context { get; set; }
		
		//发布时间
		public long CreateTime { get; set; }
		
		//更新时间
		public long UpdateTime { get; set; }

		//分类Id
		public int TypeId { get; set; }
		
		//分类名称
		public string? Type { get; set; }

		[MaxLength(3,ErrorMessage = "同一博客的标签不得多于3个")]
		//标签
		public string[] Tags { get; set; } = new string[0];
	}
}
