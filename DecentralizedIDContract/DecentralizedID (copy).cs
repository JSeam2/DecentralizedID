using Stratis.SmartContracts;

/// <summary>
/// Smart contract for decentralized ID utilizing recommendations and
/// specifications from https://w3c.github.io/did-core/ to provide a framework
/// for self-sovereign identity on Stratis. With identity established, we provide
/// support for secure payment/payout functionality where it is possible to
/// identify the entities the payment will end up in. This has uses in charity,
/// royalty payments, open source project donations, companies trying to support
/// the open startup initiative, etc.
///
/// Identity documents can come from both centralized sources (Social media,
/// government authorities) or decentralized sources. Each source has varying
/// levels of security and ease of use. The sources may be fallible with some
/// marginal probability of failure. By increasing the number of valid and verifiable
/// sources, the likelihood of the i
///
/// With the identity
///
/// Details on attack vectors is discussed in the README.md
/// </summary>
[Deploy]
public class Decentralized : SmartContract
{
    private int Index
    {
        get
        {
            return this.PersistentState.GetInt32("Index");
        }
        set
        {
            this.PersistentState.SetInt32("Index", value);
        }
    }

    private int Bounds
    {
        get
        {
            return this.PersistentState.GetInt32("Bounds");
        }
        set
        {
            this.PersistentState.SetInt32("Bounds", value);
        }
    }

    private string Greeting
    {
        get
        {
            this.Index++;
            if (this.Index >= this.Bounds)
            {
                this.Index = 0;
            }

            return this.PersistentState.GetString("Greeting" + this.Index);
        }
        set
        {
            this.PersistentState.SetString("Greeting" + this.Bounds, value);
            this.Bounds++;
        }
    }

    public HelloWorld2(ISmartContractState smartContractState) : base(smartContractState)
    {
        this.Bounds = 0;
        this.Index = -1;
        this.Greeting = "Hello World!";
    }

    public string SayHello()
    {
        return this.Greeting;
    }

    public string AddGreeting(string helloMessage)
    {
        this.Greeting = helloMessage;
        return "Added '" + helloMessage + "' as a greeting.";
    }

}

/*
 * Collection of DID Documents as a mesh of connections to verify identity
 * Extended methods like payment to distribute royalties to identities manage
 * by the DID network mesh
 * 
 * DID Document
 * {
 *   "@context": "https://www.w3.org/ns/did/v1",
 *   "id": "did:example:21tDAKCERh95uGgKbJNHYp",
 *   "publicKey": [{
       "id": "did:example:123456789abcdefghi#keys-1",
       "type": "RsaVerificationKey2018",
       "controller": "did:example:123456789abcdefghi",
       "publicKeyPem": "-----BEGIN PUBLIC KEY...END PUBLIC KEY-----\r\n"
     }, {
       "id": "did:example:123456789abcdefghi#keys-2",
       "type": "Ed25519VerificationKey2018",
       "controller": "did:example:pqrstuvwxyz0987654321",
       "publicKeyBase58": "H3C2AVvLMv6gmMNam3uVAjZpfkcJCwDwnZn6z3wXmqPV"
     }, {
       "id": "did:example:123456789abcdefghi#keys-3",
       "type": "Secp256k1VerificationKey2018",
       "controller": "did:example:123456789abcdefghi",
       "publicKeyHex": "02b97c30de767f084ce3080168ee293053ba33b235d7116a3263d29f1450936b71"
     }],
   }
 *
 *  References
 *  https://w3c.github.io/did-core/
 *  https://selfkey.org/decentralized-identifiers-article/
 */
