using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreatureTester : MonoBehaviour {

    [Header("Creature Prefab")]
    public Creature creature;

    [Header("UI Objects")]
    public GameObject load;
    public InputField loadInputField;
    public GameObject deletion;
    public GameObject save;
    public GameObject generation;
    public GameObject nameInput;
    public InputField nameInputField;
    public Text dataPath;

    [Header("Current Creature")]
    public Creature currentCreature;

    /// <summary>
    /// creates a new random creature
    /// </summary>
	public void RandomCreature()
    {
        // deactivate UI elements
        load.SetActive(false);
        generation.SetActive(false);

        // instantiate new creature
        Creature newCreature = Instantiate(creature);
        newCreature.myStats = new Creature_Stats();

        // randomize variables
        newCreature.myStats.SetColors();
        //set special feature values

        // initialize prefab
        newCreature.Initialize();

        // set reference
        currentCreature = newCreature;

        // activate name input
        nameInput.SetActive(true);
    }

    /// <summary>
    /// takes user input and names creature
    /// </summary>
    public void NameInput()
    {
        // get input text and set name value
        currentCreature.myStats.creatureName = nameInputField.text;

        // reinitialize creature
        currentCreature.Initialize();

        // deactivate input UI
        nameInput.SetActive(false);

        // activate options
        deletion.SetActive(true);
        save.SetActive(true);
    }

    public void SaveCreature()
    {
        //SaveLoad.creatures.Add(currentCreature);
        SaveLoad.Save(currentCreature);
        Destroy(currentCreature.gameObject);
        save.SetActive(false);
        deletion.SetActive(false);
        generation.SetActive(true);
        load.SetActive(true);
        dataPath.text = Application.persistentDataPath;
    }

    public void DeleteCreature()
    {
        Destroy(currentCreature.gameObject);
        generation.SetActive(true);
        load.SetActive(true);
        deletion.SetActive(false);
        save.SetActive(false);
    }

    public void LoadCreature()
    {
        Creature_Stats stats = SaveLoad.Load(loadInputField.text);
        Creature loadedCreature = Instantiate(creature);
        loadedCreature.myStats = stats;
        loadedCreature.Initialize();
        currentCreature = loadedCreature;
        dataPath.text = Application.persistentDataPath;
        generation.SetActive(false);
        load.SetActive(false);
        deletion.SetActive(true);
        save.SetActive(true);
    }
}
