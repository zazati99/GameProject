namespace GameProject.GameObjects
{
    public interface IMovable
    {

        // Moving functions used in NPCs and players
        void HorizontalMovement(float targetSpeed);
        void StopMoving();
        void Jump();

    }
}
