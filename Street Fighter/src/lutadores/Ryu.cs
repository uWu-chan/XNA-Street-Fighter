﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Street_Fighter.action;
using Street_Fighter.action.Ryu;

namespace Street_Fighter.lutadores
{
    class Ryu : Fighter
    {
        private Random random;

        public Ryu(Vector2 posicao)
            : base(posicao)
        {
            this.random = new Random();
            this.posicaoDeRepouso = new Gingando(this);

            actions.Add(this.posicaoDeRepouso);
            actions.Add(new Chute1());
            actions.Add(new Chute2());
            actions.Add(new Chute3());
            actions.Add(new Chute4());
            actions.Add(new Recuar(this));
            actions.Add(new Avancar(this));
            this.currentAction = actions[0];


        }

        public override void update(GameTime gameTime)
        {
            this.currentAction.update(gameTime);
            
            if (!this.currentAction.isRunning || this.currentAction == this.posicaoDeRepouso)
            {
                KeyboardState state = Keyboard.GetState();

                if (state.IsKeyDown(Keys.C))
                    this.currentAction = actions[this.random.Next(1, 5)];
                else if (state.IsKeyDown(Keys.Right))
                    this.currentAction = actions[5];
                else if (state.IsKeyDown(Keys.Left))
                    this.currentAction = actions[6];

            }

            // se todas as "fases" da atual ação já foram renderizadas, voltemos à posição de repouso (gingando)
            if (!this.currentAction.isRunning)
            {
                this.currentAction.reset();
                this.currentAction = this.posicaoDeRepouso;
            }

        }
    }
}