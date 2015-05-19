﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GameHelpers;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _02___Wizard
{
    [AssetNameAttr("WizardSquare")]
    class Wizard : MovingSprite, IJumper
    {
        private const int StartY = 125;

        private const int StartX = 245;

        private const float MoveSpeed = 160;

        private const int JumpHeight = 150;

        private State currState = State.Walking;

        private bool lookingRight;

        public List<Fireball> Fireballs = new List<Fireball>();

        private KeyboardState prevKeyboardState;

        //Used for jumping
        public Vector2 StartingPosition { get; set; }

        private enum State
        {
            Walking,
            Jumping,
            Ducking,
        }

        public Wizard()
            : base(new Vector2(StartX, StartY), new Vector2(MoveSpeed, 0), Vector2.Zero)
        {
            StartingPosition = new Vector2(StartX, StartY);

        }


        //LOAD
        public override void LoadContent()
        {

            Position = new Vector2(StartY, StartX);

            base.LoadContent();
            SourceOnSprite = new Rectangle(0, 0, 200, SourceOnSprite.Height);
        }


        //UPDATE
        public override void Update(GameTime theGameTime)
        {
            var currKeyboardState = Keyboard.GetState();

            if (currState != State.Jumping)
            {
                Speed = Vector2.Zero;
                Direction = Vector2.Zero;
            }

            UpdateHorizontalMovement(currKeyboardState);

            UpdateJump(currKeyboardState);

            UpdateDuck(currKeyboardState);

            UpdateFireballs(theGameTime, currKeyboardState);

            prevKeyboardState = currKeyboardState;

            base.Update(theGameTime);
        }

        //DRAW
        public override void Draw(SpriteBatch spriteBatch)
        {
            var effect = SpriteEffects.None;
            if (lookingRight)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            foreach (var aFireball in Fireballs)
            {
                aFireball.Draw(spriteBatch);
            }

            base.Draw(spriteBatch, effect);
        }



        //MOVEMENT
        private void UpdateHorizontalMovement(KeyboardState currKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.Left))
            {
                lookingRight = false;
                Speed.X = MoveSpeed;
                Direction.X = MoveDirections.MoveLeft;
            }
            else if (currKeyboardState.IsKeyDown(Keys.Right))
            {
                lookingRight = true;
                Speed.X = MoveSpeed;
                Direction.X = MoveDirections.MoveRight;
            }
        }


        //JUMP
        private void UpdateJump(KeyboardState currKeyboardState)
        {
            switch (currState)
            {
                case State.Walking:
                    if (currKeyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space))
                    {
                        currState = State.Jumping;

                        StartingPosition = Position;

                        Direction.Y = MoveDirections.MoveUp;

                        Speed = new Vector2(MoveSpeed, MoveSpeed);
                    }
                    break;

                case State.Jumping:
                    if (StartingPosition.Y - Position.Y > JumpHeight)
                    {
                        Direction.Y = MoveDirections.MoveDown;
                    }
                    if (Position.Y > StartingPosition.Y)
                    {
                        Position = new Vector2(Position.X, StartingPosition.Y);
                        currState = State.Walking;
                    }
                    break;
            }
        }


        //DUCK
        private void UpdateDuck(KeyboardState currKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                if (currState != State.Walking) return;

                SourceOnSprite = new Rectangle(200, 0, 200, SourceOnSprite.Height);
                currState = State.Ducking;
            }
            else if (currState == State.Ducking)
            {
                SourceOnSprite = new Rectangle(0, 0, 200, SourceOnSprite.Height);
                currState = State.Walking;
            }
        }

        private void UpdateFireballs(GameTime theGameTime, KeyboardState aCurrentKeyboardState)
        {
            foreach (var fireball in Fireballs)
            {
                fireball.Update(theGameTime);
            }

            Fireballs.RemoveAll(f => f.NeedsRemoval);

            if (currState != State.Walking ||
                !aCurrentKeyboardState.IsKeyDown(Keys.RightControl) ||
                prevKeyboardState.IsKeyDown(Keys.RightControl))
                return;



            var direction = lookingRight ? new Vector2(1, 0) : new Vector2(-1, 0);

            var aFireball = new Fireball(Position + new Vector2(Size.Width, Size.Height), new Vector2(200, 0), direction);
            aFireball.LoadContent();

            Fireballs.Add(aFireball);
        }

    }
}


