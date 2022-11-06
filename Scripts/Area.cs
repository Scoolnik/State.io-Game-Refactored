using System.Collections;
using TMPro;
using UnityEngine;

namespace StateIO
{
    public class Area : MonoBehaviour
    {
        public FactionId Faction => _faction;

        [SerializeField] private FactionId _faction;

        public int Unit;
        private float Timer;

        public TextMeshPro MyNumber;

        public int Manager;
        public int Attack;
        public GameObject Target;
        public GameObject UnitPrefab;
        public bool CanMake;

        public SpriteRenderer Inside;

        public GameObject OBG;
        public GameObject TouchPosition;

        private float SecondTimer;

        // Start is called before the first frame update
        private void Start()
        {
            MyNumber.text = "0";
        }

        // Update is called once per frame
        private void Update()
        {
            if (CanMake == false)
            {
                Timer += 2 * Time.deltaTime;
            }

            if (CanMake == true)
            {
                SecondTimer += 1 * Time.deltaTime;

                if (SecondTimer >= 3)
                {
                    CanMake = false;
                    SecondTimer = 0;
                }
            }


            if (Timer >= 1)
            {
                Unit += 1;
                MyNumber.text = Unit.ToString();

                Timer = 0;
            }

            if (Manager == 1)
            {
                TouchPosition.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, +10));

                GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
                GetComponent<LineRenderer>().SetPosition(1, TouchPosition.transform.position);
            }


            if (Attack == 2)
            {
                CanMake = true;
                StartCoroutine(MakeUnit());
                Attack = 3;
            }
        }

        private void OnMouseDown()
        {
            TouchPosition = Instantiate(OBG, Vector3.zero, Quaternion.identity);
            TouchPosition.GetComponent<TouchPosition>().MyAreaName = this.transform.name;

            TouchPosition.GetComponent<TouchPosition>().MyArea = this.gameObject;
            TouchPosition.GetComponent<TouchPosition>().MyTag = this.gameObject.transform.tag;

            Manager = 1;
        }

        private void OnMouseUp()
        {
            Manager = 0;
            Destroy(TouchPosition);
            GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
            GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);

            if (Attack == 1)
            {
                Attack = 2;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Tags.UnitTag))
                return;
            var unit = other.GetComponent<Unit>();
            if (unit.Sender.Faction != _faction)
            {
				Destroy(other.gameObject);

                //todo remade
				if (Unit > 0)
				{
					Unit -= 1;
					MyNumber.text = Unit.ToString();

					CanMake = true;
				}
				else
				{
					transform.tag = other.transform.tag;
                    //todo move to separate class, make event
                    var newFaction = Factions.Instance.GetFactionInfo(unit.Sender.Faction);
                    _faction = newFaction.Id;
					GetComponent<SpriteRenderer>().color = newFaction.SecondaryColor;
					Inside.color = newFaction.PrimaryColor;
				}
			}
            else
            {
				//todo bring back ability to move units from one player base to another
			}
		}

		private IEnumerator MakeUnit()
        {
            while (Unit > 0)
            {
                yield return new WaitForSeconds(0.3f);

                var unit = Instantiate(UnitPrefab, this.transform.position, Quaternion.identity).GetComponent<Unit>();
                unit.Init(this, Target.GetComponent<Area>());

                Unit -= 1;
                MyNumber.text = Unit.ToString();

                SecondTimer = 0;

                if (Unit == 0)
                {
                    Attack = 0;
                    CanMake = false;
                }
            }
        }
    }
}