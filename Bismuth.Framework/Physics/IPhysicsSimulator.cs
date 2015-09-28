using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics
{
    public interface IPhysicsSimulator
    {
        List<PhysicsBody> Bodies { get; }

        void Update(GameTime gameTime);

        //void Draw(PrimitiveBatch primitiveBatch);
    }
}
