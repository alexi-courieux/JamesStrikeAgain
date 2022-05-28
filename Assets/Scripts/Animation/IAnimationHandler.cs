using UnityEngine;

namespace ashlight.james_strike_again {
    public interface IAnimationHandler {
        /// <summary>
        /// Permet de définir la valeur d'un paramètre
        /// </summary>
        /// <param name="parameter">le nom du paramètre</param>
        /// <param name="parameterValue">la valeur du paramètre, supporte tout les types compatibles avec l'animator. Si non défini on considère qu'il s'agit d'un trigger.</param>
        public void SetParameter(string parameter, object parameterValue = null);
        /// <summary>
        /// Surcharge le controller avec un <see cref="AnimatorOverrideController"/>, permettant de changer les animations et les transitions
        /// </summary>
        /// <param name="animatorController">La surcharge du controller</param>
        public void SetController(AnimatorOverrideController animatorController);
    }
}