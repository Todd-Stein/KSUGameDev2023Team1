using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleAttach : MonoBehaviour
{
    private Interactable interActable;
    private void Start()
    {
        interActable = GetComponent<Interactable>();
    }

    private void onHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }


    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        //grab it
        if (interActable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(interActable);

        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interActable);
        }
    }

    //-------------------------------------------------
    private void onAttachedToHand(Hand hand)
    {
        
    }


    //-------------------------------------------------
    private void onDetachedFromHand(Hand hand)
    {
        

    }
}
