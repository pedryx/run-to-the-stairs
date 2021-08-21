using System.Numerics;


namespace GameLib
{

    /// <summary>
    /// Represent a trasformation information.
    /// </summary>
    public class Transform : IComponent
    {
        private bool dirty_ = true;
        private IMatrix matrix_;

        private Vector2 position_;
        private float rotation_;
        private Vector2 scale_ = Vector2.One;

        public Vector2 Position
        {
            get => position_;
            set
            {
                if (position_ == value)
                    return;

                position_ = value;
                dirty_ = true;
            }
        }

        public float Rotation
        {
            get => rotation_;
            set
            {
                if (rotation_ == value)
                    return;

                rotation_ = value;
                dirty_ = true;
            }
        }

        public Vector2 Scale
        {
            get => scale_;
            set
            {
                if (scale_ == value)
                    return;

                scale_ = value;
                dirty_ = true;
            }
        }

        public IMatrix GetMatrix<TMatrix>()
            where TMatrix : IMatrix, new()
        {
            if (dirty_)
            {
                IMatrix transform = new TMatrix();
                transform.SetTranslation(Position);
                transform.SetScale(Scale);

                var rotation = new TMatrix();
                rotation.SetRotation(Rotation);

                matrix_ = transform.Multiply(rotation);
                dirty_ = false;
            }

            return matrix_;
        }

    }
}
