using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestBall
    {
        public Ball TestBallFactory(Paddle paddle, Vector2 initialPosition, bool hasBeenShot = false)
        {
            var gameObject = new GameObject();
            Ball ball = gameObject.AddComponent<Ball>();
            
            // adds state to the object
            ball.gameObject.AddComponent<Rigidbody2D>();
            ball.paddle = paddle;
            ball.transform.position = initialPosition;
            ball.hasBallBeenShot = false;
            
            return ball;
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void TestFixBallOnPaddle_ShouldUpdateBallPositionToTop()
        {
            var paddle = new TestPaddle().TestPaddleFactory(new Vector2(2f, 2f));
            var ball = TestBallFactory(paddle, new Vector2(2f, 3f), false);
            var distanceToTopOfPaddle = ball.transform.position - paddle.transform.position;

            // assumes paddle has moved on the X axis 2 float units
            var newPaddlePosition = new Vector2(4f, 2f);

            // method invocation
            ball.FixBallOnTopOfPaddle(newPaddlePosition, distanceToTopOfPaddle);
            
            // ball should be ON TOp at 4f, 3f!
            Assert.AreEqual(new Vector3(4f, 3f, 0f), ball.transform.position);
        }

        [Test]
        public void TextShootBallOnClick_ShouldSetBallSpeed()
        {
            var paddle = new TestPaddle().TestPaddleFactory(new Vector2(2f, 2f));
            var ball = TestBallFactory(paddle, new Vector2(2f, 3f), false);
            var initialBallSpeed = new Vector2(2f, 10f);
            ball.initialBallSpeed = initialBallSpeed;
            

            // cannot be shot at the beginning
            Assert.IsFalse(ball.hasBallBeenShot);
            
            // method invocation
            ball.ShootBallOnClick(initialBallSpeed, hasMouseClick: true);  // fakes mouse click
            
            // assertions
            Assert.IsTrue(ball.hasBallBeenShot);
            Assert.AreEqual(initialBallSpeed, ball.gameObject.GetComponent<Rigidbody2D>().velocity);
        }
    }
}
