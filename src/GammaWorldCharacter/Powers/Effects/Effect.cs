using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Origins;

// Aim:
// 1. Create easier synatx to describe power effects that (1) actually
//    describes the effects rather than just including it as text and
//    (2) is easy to read and understand effect syntax. Fluent is good
//    but not required (strongly influenced by NUnit).
// 2. Be easy to convert into human readable text.
// 3. Allow other uses, such as in a game.
//
// Want to be able to write something like:
//
// Effect.Creature.Damage(1.D10()).And.SameTarget.GrantsCombatAdvantage(Until.EncounterEnd); // Android critical
// Effect.Creature.Damage(1.D10()).And.You.GainBonus(Score(ScoreType.AC), 4, Until.EndOfNextTurn); // Cockroach critical
// Effect.Creature.Damage(1.D10()).And.You.CanUsePower(typeof(DoubleTrouble, ActionType.Free); // DoppelGanger critical
// Effect.Creature.Damage(1.D10()).And.Ally(Where.WithinSquares(5, Of.Target).GainsBonus(ScoreType.TemporaryHitPoints, 10); // Electrokinetic critical
// Effect.Ally(Where.WithinSquares(5)).GainsTemporaryHitPoints(You.Level.Times(2); // Empath critical
// Effect.Creature.Damage(1.D10()).And.You.Shift(3, ActionType.Free); // Felinoid critical
// Effect.Creature.Damage(1.D10()).And.SameTarget.Push(3); // Giant critical
// Effect.Creature.Damage(1.D10()).And.Creature(Where.WithinSquares(2, Of.Target)).Immobilized(Until.EndOfNextTurn); // Gravity Controller critical
// Effect.Creature.Damage(1.D10()).And.You.CanFly(Score(ScoreType.Speed), ActionType.Free); // Hawkoid critical
// Effect.Creature.Damage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsBonusToAllDefenses(2, Until.EndOfNextTurn); // Hypercognitive critical
//
// Form:
// Target (subject; either Creature, You, Ally, YouOrAlly, Enemy or SameTarget (property only, indicates same target as previous expression) 
//    -> Effect (e.g. Damage, GrantsCombatAdvantage, GainBonus, Push, CanFly, etc) (verb and object) 
//    (optional) -> Conjunction -> Target -> Effect (etc) ...
//
// * Need to be careful to not leave out required parts, therefore moving some parts to arguments.
//
// Current thoughts:
// * Does "Effect" become a static class to start the chain or should Effect merge with EffectComponent?

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// An effect, such as damage or healing, on a target, such as a creature or an ally. It may
    /// include mulitple <see cref="EffectComponent"/>s, each acting on the same or different
    /// targets.
    /// </summary>
    public static class Effect
    {
        /// <summary>
        /// A creature.
        /// </summary>
        public static Target Creature
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.Creature, null);
            }
        }

        /// <summary>
        /// The power originator.
        /// </summary>
        public static Target You
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.You, null);
            }
        }

        /// <summary>
        /// A friendly target
        /// </summary>
        public static Target Ally(Where where)
        {
            return new Target(new EffectExpression(), TargetType.Ally, where);
        }

        /// <summary>
        /// Either the power originator or a friendly target.
        /// </summary>
        public static Target YouOrAlly(Where where)
        {
            return new Target(new EffectExpression(), TargetType.YouOrAlly, where);
        }

        /// <summary>
        /// An unfriendly target.
        /// </summary>
        public static Target Enemy(Where where)
        {
            return new Target(new EffectExpression(), TargetType.Enemy, where);
        }
    }
}
