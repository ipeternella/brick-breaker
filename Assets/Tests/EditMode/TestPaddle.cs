using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestPaddle
    {

        [Test]
        public void TestShouldReturnUpdatedPaddlePositionVector()
        {
            // creates a new paddle game object
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            
            // method invocation
            Vector2 vector = paddle.GetUpdatedPaddlePosition(10f);
            Vector2 expectedVector = new Vector2(10f, 0.34f );

            Assert.AreEqual(vector, expectedVector);
        }
        
        [Test]
        public void TestShouldConvertPixelToRelativeUnitPosition_FarthestX()
        {
            // creates a new paddle game object with its state
            int fakeScreenWidth = 32;
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.screenWidthUnits = 16;
            
            // method invocation
            float relativeValue = paddle.ConvertPixelToRelativePosition(32, fakeScreenWidth);  // farthest X possible
            
            // assertions
            Assert.AreEqual(relativeValue, 16f);
        }
        
        [Test]
        public void TestShouldConvertPixelToRelativeUnitPosition_SmallestX()
        {
            // creates a new paddle game object with its state
            int fakeScreenWidth = 32;
            Paddle paddle = new GameObject().AddComponent<Paddle>();
            paddle.screenWidthUnits = 16;
            
            // method invocation
            float relativeValue = paddle.ConvertPixelToRelativePosition(0, fakeScreenWidth);
            
            // assertions
            Assert.AreEqual(relativeValue, 0);
        }
    }
}
