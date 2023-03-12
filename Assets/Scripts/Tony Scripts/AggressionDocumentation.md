# Tony Aggression Scripts

- Aggression is increased each time Tony is alerted that he sees the player. In addition, it increases more as the player stays in his vision pyramid.
- Aggression starts at 40. Meant to be out of 100.
- Speed is meant to be equal to aggression/10. Increasing has a lot of variety for each possible object and event.
- Listening radius increases with aggression.
- Aggression increases can be called with `aggroIncrease(int value)`. Value being the amount to increase by. Speed is also changed in accordance with hunting or not, along with the size of the listening radius.
