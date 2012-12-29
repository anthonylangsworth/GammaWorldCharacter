using System;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// The type of damage dealt.
    /// </summary>
    [Flags]
    public enum DamageTypes
    {
        /// <summary>
        /// The power does not do damage.
        /// </summary>
        None = 0,
        /// <summary>
        /// Corrosive liquid or gas.
        /// </summary>
        Acid = 1,
        /// <summary>
        /// Ice crystals, frigid air or frigid liquid.
        /// </summary>
        Cold = 2,
        /// <summary>
        /// Electrical energy.
        /// </summary>
        Electricity = 4,
        /// <summary>
        /// Explosive bursts, fiery rays or simple ignition.
        /// </summary>
        Fire = 8,
        /// <summary>
        /// Invisible energy formed into incredibly had yet nonsolid shapes.
        /// </summary>
        Force = 16,
        /// <summary>
        /// Laser or beams of intense electromagnetic radiation.
        /// </summary>
        Laser = 16,
        /// <summary>
        /// Purple-black energy that deadens flesh and wounds the soul.
        /// </summary>
        Necrotic = 32,
        /// <summary>
        /// The power does physical (i.e. normal weapon) damage.
        /// </summary>
        Physical = 64,
        /// <summary>
        /// Toxins that reduce a Character's hit points.
        /// </summary>
        Poison = 128,
        /// <summary>
        /// Effects that target the mind.
        /// </summary>
        Psychic = 256,
        /// <summary>
        /// Radiation, usually ionizing.
        /// </summary>
        Radiation = 512,
        /// <summary>
        /// Shock waves and deadening sounds.
        /// </summary>
        Sonic = 1024
    }
}
