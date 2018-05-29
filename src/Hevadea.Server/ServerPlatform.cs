using Hevadea.Framework;
using Hevadea.Framework.Platform;
using System;

namespace Hevadea.Server
{
    public class ServerPlatform : PlatformBase
    {
        public override string GetPlatformName() => "Server";

        public override int GetScreenWidth() => 0;

        public override int GetScreenHeight() => 0;

        public override string GetStorageFolder()
        {
            return ".";
        }

        public override void Stop()
        {
            Rise.MonoGame.Exit();
        }

        public override void Initialize()
        {
            try
            {
                Console.Title = $"{Game.Name} {Game.Version}";
            }
            catch (Exception)
            {

            }
        }

        public override void Update()
        {
        }
    }
}