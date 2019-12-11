using System;
using System.Linq;
using Stratis.SmartContracts;

[Deploy]
public class DecentralizedID : SmartContract
{

    public Address SmartContractOwner
    {
        get
        {
            return this.PersistentState.GetAddress("SmartContractOwner");
        }
        private set
        {
            this.PersistentState.SetAddress("SmartContractOwner", value);
        }
    }

    /// <summary>
    /// Index for DID
    /// </summary>
    private ulong Index
    {
        get
        {
            return this.PersistentState.GetUInt64("Index");
        }
        set
        {
            this.PersistentState.SetUInt64("Index", value);
        }
    }

    /// <summary>
    /// Get the owner's address given a DID address
    /// </summary>
    /// <param name="DIDIndex">Index of DID</param>
    /// <returns>Owner's address given the DIDIndex provided</returns>
    public Address GetOwnerOfDID(ulong DIDIndex)
    {
        return this.PersistentState.GetAddress($"Owner:{DIDIndex}");
    }

    /// <summary>
    /// Set the owner's address to link to a DID address
    /// </summary>
    /// <param name="DIDIndex">Index of DID</param>
    /// <param name="ownerAddress">Owner of DID wallet address</param>
    private void SetOwnerOfDID(ulong DIDIndex, Address ownerAddress)
    {
        this.PersistentState.SetAddress($"Owner:{DIDIndex}", ownerAddress);
    }

    /// <summary>
    /// Get the string data of the DID following the specifications on
    /// https://www.w3.org/TR/did-core we take in string data for extensibility
    /// </summary>
    /// <param name="DIDIndex">Index of DID</param>
    /// <returns>String data associated with the DID</returns>
    public string GetDataOfDID(ulong DIDIndex)
    {
        return this.PersistentState.GetString($"DIDData:{DIDIndex}");
    }

    /// <summary>
    /// Set the string data of the DID following the specifications on
    /// https://www.w3.org/TR/did-core we take in string data for extensibility
    /// </summary>
    /// <param name="DIDIndex">Index of DID</param>
    /// <param name="data">String data associated with the DID</param>
    private void SetDataOfDID(ulong DIDIndex, string data)
    {
        this.PersistentState.SetString($"DIDData:{DIDIndex}", data);
    }

    //public ulong[] GetOwnerDIDs(Address ownerAddress)
    //{
    //    ulong[] data = this.PersistentState.GetArray<ulong>($"DIDArray:{ownerAddress}");

    //    return data;

    //}

    //public void SetOwnerDIDs(Address ownerAddress, ulong[] value)
    //{
    //    //byte[] SerializedData = Serializer.Serialize(value);
    //    this.PersistentState.SetArray($"DIDArray:{ownerAddress}", value);
    //}

    public DecentralizedID(ISmartContractState smartContractState)
        : base(smartContractState)
    {
        this.Index = 0;
        this.SmartContractOwner = this.Message.Sender;
    }

    /// <summary>
    /// Create and associate DID
    /// </summary>
    /// <param name="data">String data associated with the DID</param>ma
    public void CreateDID(string data)
    {
        SetDataOfDID(this.Index, data);
        SetOwnerOfDID(this.Index, this.Message.Sender);

        // Handle DID Array
        //ulong[] DIDArray = GetOwnerDIDs(this.Message.Sender);
        //Array.Resize(ref DIDArray, DIDArray.Length + 1);
        //DIDArray[DIDArray.Length] = this.Index;
        //SetOwnerDIDs(this.Message.Sender, DIDArray);

        Log(new CreateDIDEvent {
            DIDIndex = this.Index,
            DIDOwner = this.Message.Sender.ToString(),
            active = true
        });

        this.Index++;

    }

    /// <summary>
    /// Revoke DID and set string data to empty string and set OwnerAddress to
    /// Zero
    /// </summary>
    /// <param name="data">String data associated with the DID</param>
    public void RevokeDID(ulong DIDIndex)
    {
        // Check if the DID belongs to the sender
        Assert(GetOwnerOfDID(DIDIndex) == this.Message.Sender);

        SetDataOfDID(DIDIndex, "");
        SetOwnerOfDID(DIDIndex, Address.Zero);

        // Handle DID Array
        //ulong[] DIDArray = GetOwnerDIDs(this.Message.Sender);
        //int numIndex = Array.IndexOf(DIDArray, DIDIndex);
        //DIDArray = DIDArray.Where((val, idx) => idx != numIndex).ToArray();
        //SetOwnerDIDs(this.Message.Sender, DIDArray);

        Log(new RevokeDIDEvent
        {
            DIDIndex = DIDIndex,
            DIDOwner = this.Message.Sender.ToString(),
            active = false
        });
    }

    public struct CreateDIDEvent
    {
        [Index]
        public ulong DIDIndex;
        public string DIDOwner;
        public bool active;
    }

    public struct RevokeDIDEvent
    {
        [Index]
        public ulong DIDIndex;
        public string DIDOwner;
        public bool active;
    }
}