using System.Linq;
using Framework.Core;
using Framework.Toolkits.FluentAPI;
using UnityEngine;

namespace Game
{
    public class AimHelper
    {
        public static IEnemy GetClosestEnemy(Transform self, Vector2 worldPosition)
        {
            IEnemy enemy = null;
            var currentRoom = Player.Instance.GetModel<LevelModel>().CurrentRoom;
            if (currentRoom && currentRoom.EnemiesInRoom.Count > 0)
            {
                enemy = currentRoom.EnemiesInRoom
                    .OrderBy(e => ((Vector2)e.Position - worldPosition).magnitude)
                    .FirstOrDefault(e =>
                    {
                        var vector2 = self.Position2DTo(e.Position);
                        if (Physics2D.Raycast(self.GetPosition(), vector2.normalized, vector2.magnitude,
                                LayerMask.GetMask("Wall")))
                        {
                            return false;
                        }

                        return true;

                        return e.GameObject.name.StartsWith("Enemy"); // 仅对小怪生效
                    });
            }

            return enemy;
        }
    }
}