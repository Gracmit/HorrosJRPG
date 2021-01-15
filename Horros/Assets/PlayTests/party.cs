using NUnit.Framework;
using UnityEngine;

namespace Player
{
    public class party
    {
        [Test]
        public void can_add_member_to_party_pool()
        {
            var pool = new GameObject("PartyPool").AddComponent<PartyPool>();
            var member = new PartyMember();
            
            pool.AddMember(member);
            
            Assert.AreEqual(1, pool.MembersCount);
        }
        
        [Test]
        public void can_add_multiple_members_to_party_pool()
        {
            var pool = new GameObject("PartyPool").AddComponent<PartyPool>();
            var member = new PartyMember();
            var member2 = new PartyMember();
            
            pool.AddMember(member);
            pool.AddMember(member2);
            
            Assert.AreEqual(2, pool.MembersCount);
        }
        
        [Test]
        public void adds_same_member_only_once()
        {
            var pool = new GameObject("PartyPool").AddComponent<PartyPool>();
            var member = new PartyMember();
            pool.AddMember(member);
            pool.AddMember(member);
            
            Assert.AreEqual(1, pool.MembersCount);
        }
        
        [Test]
        public void can_remove_a_member()
        {
            var pool = new GameObject("PartyPool").AddComponent<PartyPool>();
            var member = new PartyMember();
            pool.AddMember(member);

            Assert.AreEqual(1, pool.MembersCount);
            
            pool.RemoveMember(member);
            Assert.AreEqual(0, pool.MembersCount);
        }
        
        [Test]
        public void does_not_remove_if_member_is_not_in_pool()
        {
            var pool = new GameObject("PartyPool").AddComponent<PartyPool>();
            var member = new PartyMember();
            var member2 = new PartyMember();
            pool.AddMember(member);

            Assert.AreEqual(1, pool.MembersCount);
            
            pool.RemoveMember(member2);
            Assert.AreEqual(1, pool.MembersCount);
        }
        
        [Test]
        public void can_equip_an_equipment_for_member()
        {
            var weapon = new GameObject("Weapon").AddComponent<Weapon>();
            var armor = new GameObject("Armor").AddComponent<Armor>();
            var accessory = new GameObject("Accessory").AddComponent<Accessory>();
            var member = new PartyMember();
            
            member.Equip(weapon);
            Assert.AreEqual(weapon, member.Weapon);
            member.Equip(armor);
            Assert.AreEqual(armor, member.Armor);
            member.Equip(accessory);
            Assert.AreEqual(accessory, member.Accessory);
        }
        
        [Test]
        public void can_unequip_an_equipment_for_member()
        {
            var weapon = new GameObject("Weapon").AddComponent<Weapon>();
            var armor = new GameObject("Armor").AddComponent<Armor>();
            var accessory = new GameObject("Accessory").AddComponent<Accessory>();
            var member = new PartyMember();
            
            member.Equip(weapon);
            member.Equip(armor);
            member.Equip(accessory);
            member.UnEquipWeapon();
            member.UnEquipArmor();
            member.UnEquipAccessory();
            Assert.IsNull(member.Weapon);
            Assert.IsNull(member.Armor);
            Assert.IsNull(member.Accessory);
            
        }
    }
}