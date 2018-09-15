using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Types;
using Healthy.Infrastructure.Settings;

namespace Healthy.Application.Services.Users
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;
        private readonly FaceBookSettings _facebookSettings;

        public FacebookService(IFacebookClient facebookClient, FaceBookSettings facebookSettings)
        {
            _facebookClient = facebookClient;
            _facebookSettings = facebookSettings;
        }

        public async Task<Maybe<FacebookUser>> GetUserAsync(string accessToken)
        {
            var user = await _facebookClient.GetAsync<dynamic>(
                "me", accessToken, "fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale");
            if (user == null)
                return new Maybe<FacebookUser>();

            var facebookUser = new FacebookUser
            {
                Id = user.id,
                Email = user.email,
                Name = user.name,
                UserName = user.username,
                FirstName = user.first_name,
                LastName = user.last_name,
                PublicToken = accessToken,
                Locale = user.locale
            };

            return facebookUser;
        }

        public async Task<bool> ValidateTokenAsync(string accessToken)
        {
            try
            {
                var user = await GetUserAsync(accessToken);

                return user.HasValue;
            }
            catch
            {
                return false;
            }
        }

        public async Task PostOnWallAsync(string accessToken, string message)
            => await _facebookClient.PostAsync("me/feed", accessToken, new {message});

        public string GetAvatarUrl(string facebookId)
            => $"https://graph.facebook.com/{facebookId}/picture?type=large";
  }
}