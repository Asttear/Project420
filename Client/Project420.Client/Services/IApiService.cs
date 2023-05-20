using Project420.Shared.Models;

namespace Project420.Client.Services;

public interface IApiService
{
    Task<IList<CommentModel>> GetCommentsAsync(string articleId, int count);
    Task AddCommentAsync(string articleId, string comment);
    Task<IList<ArticleMetadata>> ListArticlesAsync(int count);
    Task<ArticleModel> GetArticleAsync(string id);
    Task<IList<CaseMetadata>> ListCasesAsync(int count);
    Task<CaseModel> GetCaseAsync(string id);
    Task<IList<DiscussionMetadata>> ListDiscussionsAsync(int count);
    Task<DiscussionModel> GetDiscussionAsync(string id);
    Task AddDiscussionAsync(string title, string htmlContent);
    Task<ProfileModel> GetProfileAsync();
    Task<ProfileModel> GetProfileAsync(string userName);
    Task<ProfileModel> ModifyProfileAsync(ProfileModel profile);
    Task<IList<LinkModel>> GetLinksAsync();
}
