using UnityEngine;

namespace KartGame.KartSystems {

    public class AntiControllerInput : BaseInput
    {
        public string TurnInputName = "";
        public string AccelerateButtonName = "";
        public string BrakeButtonName = "";
        public string InteractButtonName = "";

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = Input.GetButton(AccelerateButtonName),
                Brake = Input.GetButton(BrakeButtonName),
                TurnInput = Input.GetAxis("Horizontal"),
                InteractInput = Input.GetButton(InteractButtonName)
            };
        }
    }
}