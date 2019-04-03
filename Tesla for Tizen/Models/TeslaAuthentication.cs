using System;
using Tesla.NET.Models;
using TeslaTizen.Utils;

namespace TeslaTizen.Models
{
    public class TeslaAuthentication
    {
        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the type of the <see cref="AccessToken"/>.
        /// </summary>
        public string TokenType { get; }

        /// <summary>
        /// Gets the expiry duration in seconds of the <see cref="AccessToken"/>.
        /// </summary>
        public long ExpiresIn { get; }

        /// <summary>
        /// Gets the expiry duration of the <see cref="AccessToken"/>.
        /// </summary>
        public TimeSpan ExpiresInTimespan => TimeSpan.FromSeconds(ExpiresIn);

        /// <summary>
        /// Gets the UTC <see cref="DateTime"/> when the <see cref="AccessToken"/> expires.
        /// </summary>
        public DateTime ExpiresUtc => EpochConversion.FromSeconds(CreatedAt + ExpiresIn);

        /// <summary>
        /// Gets the Epoch timestamp when the <see cref="AccessToken"/> was issued.
        /// </summary>
        public long CreatedAt { get; }

        /// <summary>
        /// Gets the UTC <see cref="DateTime"/> when the <see cref="AccessToken"/> was issued.
        /// </summary>
        public DateTime CreatedUtc => EpochConversion.FromSeconds(CreatedAt);

        /// <summary>
        /// Gets the refresh token that can be used to acquire a new <see cref="AccessToken"/>.
        /// </summary>
        public string RefreshToken { get; }

        private string DebuggerDisplay => $"{GetType().Name}: {AccessToken.Substring(0, 6)}… Expires {ExpiresUtc:R}";

        public TeslaAuthentication(IAccessTokenResponse accessTokenResponse)
        {
            AccessToken = accessTokenResponse.AccessToken;
            TokenType = accessTokenResponse.TokenType;
            CreatedAt = accessTokenResponse.CreatedAt;
            ExpiresIn = accessTokenResponse.ExpiresIn;
            RefreshToken = accessTokenResponse.RefreshToken;
        }

        public TeslaAuthentication(string accessToken,
                                   string tokenType,
                                   long expiresIn,
                                   long createdAt,
                                   string refreshToken)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            TokenType = tokenType ?? throw new ArgumentNullException(nameof(tokenType));
            ExpiresIn = expiresIn;
            CreatedAt = createdAt;
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
        }
    }
}
