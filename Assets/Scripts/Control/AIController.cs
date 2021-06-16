using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
	public class AIController : MonoBehaviour
	{
		[SerializeField] float chaseDistance = 5f;

		Fighter fighter;
		GameObject player;
		Health health;
		Mover mover;

		Vector3 guardLocation;

		private void Start()
		{
			fighter = GetComponent<Fighter>();
			player = GameObject.FindWithTag("Player");
			health = GetComponent<Health>();
			mover = GetComponent<Mover>();

			guardLocation = transform.position;
		}

		private void Update()
		{
			if (health.IsDead()) return;

			if (InAttackRangeOfPlayer()  && fighter.CanAttack(player))
			{
				fighter.Attack(player);
			}
			else
			{
				mover.StartMoveAction(guardLocation);
			}
		}

		private bool InAttackRangeOfPlayer()
		{
			if (player == null) return false;
			return Vector3.SqrMagnitude(player.transform.position - transform.position) <= chaseDistance * chaseDistance;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, chaseDistance);
		}
	}
}
