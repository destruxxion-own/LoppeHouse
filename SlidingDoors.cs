using UnityEngine;

namespace LoppeHouse
{
    internal class SlidingDoors
    {
        private static Transform Drawer1, Drawer2, Drawer3;
        private static BoxCollider DrawerCollider1, DrawerCollider2, DrawerCollider3;
        public static void InitializeTriggers()
        {
            Drawer1 = GameObject.Find("Animations(Clone)/Door10/ObjectsTrigger").transform;
            Drawer2 = GameObject.Find("Animations(Clone)/Door11/ObjectsTrigger").transform;
            Drawer3 = GameObject.Find("Animations(Clone)/Door12/ObjectsTrigger").transform;

            DrawerCollider1 = Drawer1.GetComponent<BoxCollider>();
            DrawerCollider2 = Drawer2.GetComponent<BoxCollider>();
            DrawerCollider3 = Drawer3.GetComponent<BoxCollider>();
        }
        public static void CheckCollision()
        {
            GameObject[] allPartsObjects = GameObject.FindGameObjectsWithTag("PART");
            foreach (GameObject part in allPartsObjects)
            {
                Collider partCollider = part.GetComponent<Collider>();

                if (partCollider != null)
                {
                    if (DrawerCollider1.bounds.Intersects(partCollider.bounds))
                    {
                        part.transform.SetParent(Drawer1);
                    }
                    else if (DrawerCollider2.bounds.Intersects(partCollider.bounds))
                    {
                        part.transform.SetParent(Drawer2);
                    }
                    else if (DrawerCollider3.bounds.Intersects(partCollider.bounds))
                    {
                        part.transform.SetParent(Drawer3);
                    }
                }
            }

            GameObject[] allItemsObjects = GameObject.FindGameObjectsWithTag("ITEM");
            foreach (GameObject part in allItemsObjects)
            {
                Collider partCollider = part.GetComponent<Collider>();

                if (partCollider != null)
                {
                    if (DrawerCollider1.bounds.Intersects(partCollider.bounds))
                    {
                        part.transform.SetParent(Drawer1);
                    }
                    else if (DrawerCollider2.bounds.Intersects(partCollider.bounds))
                    {
                        part.transform.SetParent(Drawer2);
                    }
                    else if (DrawerCollider3.bounds.Intersects(partCollider.bounds))
                    {
                        part.transform.SetParent(Drawer3);
                    }
                }
            }
        }
    }
}
