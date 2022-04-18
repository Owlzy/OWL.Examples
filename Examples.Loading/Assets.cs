using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json.Linq;
using OWL.Texture;

namespace Examples.Loading
{
    public class Assets
    {
        public ContentManager Content { get; protected set; }
        public GraphicsDevice GraphicsDevice { get; protected set; }

        protected IDictionary<string, JToken> jsonData = new Dictionary<string, JToken>();

        protected Effect spineEffect;
        public Effect ProjectionEffect { get; protected set; }

        public Assets(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Content = content;
            GraphicsDevice = graphicsDevice;
        }

        /// <summary>
        ///     Loads a texture atlas
        /// </summary>
        /// <param name="assetName">The name of the asset <see cref="string" /> to be loaded.</param>
        /// <returns>The asset loaded <see cref="TextureAtlas" />.</returns>
        public TextureAtlas Load(string assetName, out TextureAtlas atlas)
        {
            atlas = Content.Load<TextureAtlas>(assetName);
            TextureAtlas.TextureCache.Add(atlas);
            return atlas;
        }

        /// <summary>
        ///     Loads a texture atlas
        /// </summary>
        /// <param name="assetName">The name of the asset <see cref="string" /> to be loaded.</param>
        /// <returns>The asset loaded <see cref="TextureAtlas" />.</returns>
        public TextureAtlas Load(string assetName, out TextureAtlas atlas, ContentManager content)
        {
            atlas = content.Load<TextureAtlas>(assetName);
            TextureAtlas.TextureCache.Add(atlas);
            return atlas;
        }

        /// <summary>
        ///     Loads a texture
        /// </summary>
        /// <param name="assetName">The name of the asset <see cref="string" /> to be loaded.</param>
        /// <returns>The asset loaded <see cref="Texture" />.</returns>
        public Texture2D Load(string uri, out Texture2D texture)
        {
            string[] split = uri.Split('/');
            string name = split[split.Length - 1];

            texture = Content.Load<Texture2D>(uri);

            TextureAtlas.TextureCache.Add(name, new TextureRegion2D(texture));

            return texture;
        }

        /// <summary>
        ///     Loads a json object
        /// </summary>
        /// <param name="uri">The uri of the asset <see cref="string" /> to be loaded.</param>
        /// <returns>The asset loaded <see cref="JToken" />.</returns>
        public JToken Load(string uri, out JToken data)
        {
            string[] split = uri.Split('/');
            string name = split[split.Length - 1];

            data = Content.Load<JToken>(uri);

            jsonData.Add(name, data);

            return data;
        }

        /// <summary>
        ///     Loads sprite font
        /// </summary>
        /// <param name="uri">The uri of the asset <see cref="string" /> to be loaded.</param>
        /// <returns>The asset loaded <see cref="SpriteFont" />.</returns>
        public SpriteFont Load(string uri, out SpriteFont font)
        {
            font = Content.Load<SpriteFont>(uri);
            return font;
        }

        /// <summary>
        ///     Loads a song
        /// </summary>
        /// <param name="uri">The uri of the asset <see cref="string" /> to be loaded.</param>
        /// <returns>The asset loaded <see cref="Song" />.</returns>
        public Song Load(string uri, out Song music)
        {
            music = Content.Load<Song>(uri);
            return music;
        }

        /// <summary>
        ///     Gets a sprite font
        /// </summary>
        /// <param name="uri">The name of the asset <see cref="string" /> to be retrieved.</param>
        /// <returns>The asset <see cref="SpriteFont" />.</returns>
        public SpriteFont GetSpriteFont(string name)
        {
            var spriteFont = Content.Load<SpriteFont>("fonts/" + name);
            return spriteFont;
        }

        /// <summary>
        ///     Gets a texture region
        /// </summary>
        /// <param name="uri">The name of the asset <see cref="string" /> to be retrieved.</param>
        /// <returns>The asset <see cref="TextureRegion2D" />.</returns>
        public TextureRegion2D GetTextureRegion(string name)
        {
            return TextureAtlas.TextureCache.GetRegion(name);
        }

        /// <summary>
        ///     Gets a json object
        /// </summary>
        /// <param name="uri">The name of the asset <see cref="string" /> to be retrieved.</param>
        /// <returns>The asset <see cref="JToken" />.</returns>
        public JToken GetJson(string name)
        {
            if (!jsonData.ContainsKey(name))
                throw new KeyNotFoundException();

            return (JObject)jsonData[name];
        }

        /// <summary>
        ///     Gets a json array
        /// </summary>
        /// <param name="uri">The name of the asset <see cref="string" /> to be retrieved.</param>
        /// <returns>The asset <see cref="JArray" />.</returns>
        public T GetJson<T>(string name) where T : JArray
        {
            if (!jsonData.ContainsKey(name))
                throw new KeyNotFoundException();

            return (T)jsonData[name];
        }
    }
}
