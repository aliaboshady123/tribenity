using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
	public class PlayerController : MonoBehaviour
	{
		Mover mover;
		Fighter fighter;

		private void Start()
		{
			mover = GetComponent<Mover>();
			fighter = GetComponent<Fighter>();
		}

		private void Update()
		{
			if (InteractWithCombat()) return;
			if (InteractWithMovement()) return;
		}

		private bool InteractWithCombat()
		{
			RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
			foreach (RaycastHit hit in hits)
			{
				CombatTarget target = hit.transform.GetComponent<CombatTarget>();
				if (!GetComponent<Fighter>().CanAttack(target)) continue;

				if(Input.GetMouseButtonDown(0)) fighter.Attack(target);
				return true;
			}
			return false;
		}

		private bool InteractWithMovement()
		{	
			RaycastHit hit;
			bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

			if (hasHit)
			{
				if (Input.GetMouseButton(0)) { 
					mover.StartMoveAction(hit.point);
					//fighter.Cancel();
				}
				return true;
			}
			return false;
		}

		private static Ray GetMouseRay()
		{
			return Camera.main.ScreenPointToRay(Input.mousePosition);
		}
	}
}
