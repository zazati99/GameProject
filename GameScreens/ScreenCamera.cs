using GameProject.GameObjects;
using GameProject.GameUtils;

namespace GameProject.GameScreens
{
    public class ScreenCamera
    {
        // Target of camera
        GameObject target;

        // Speed of camerea
        float speed;

        // Initialize stuff
        public void Initialize()
        {
            speed = .1f;
        }

        // Update camera
        public void Update()
        {
            if (target != null)
            {
                GameView.SetPosition(GameMath.Lerp(GameView.GetPosition(), target.Position, speed));
            }
        }

        // Set target
        public void SetTarget(GameObject gameObject)
        {
            target = gameObject;
        }
    }
}
