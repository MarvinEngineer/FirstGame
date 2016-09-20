using UnityEngine;
using System;

public class CubeView : MonoBehaviour, ICubeView
{

    public event EventHandler<FieldEventArgs> FieldUpdated = delegate { };

    private Field thisField;

    void Awake()
    {
        thisField = new Field(Convert.ToInt32(char.GetNumericValue(name[0])), Convert.ToInt32(char.GetNumericValue(name[1])), field.e);
    }

    void Start()
    {
        Change("empty");
    }

    void OnMouseEnter()
    {
        Change("over");
    }

    void OnMouseOver()
    {
        Change("select");
        if ((Input.GetMouseButton(0)) && (thisField.content == field.e))
        {
            Change("cross");
            FieldUpdated(this, new FieldEventArgs(thisField));
        }
    }

    void OnMouseExit()
    {
        Change("unselect");
    }

    private void Change(string _c)
    {
        switch (_c)
        {
            case "empty":
                {
                    thisField.content = field.e;
                    GetComponent<Renderer>().materials[0].color = Color.white;
                    break;
                }
            case "cross":
                {
                    thisField.content = field.X;
                    GetComponent<Renderer>().materials[0].color = Color.red;
                    break;
                }
            case "nought":
                {
                    thisField.content = field.O;
                    GetComponent<Renderer>().materials[0].color = Color.blue;
                    break;
                }
            case "select":
                {
                    if (thisField.content == field.e) GetComponent<Renderer>().materials[0].color = new Color32(255, 164, 0, 0);
                    break;
                }
            case "unselect":
                {
                    if (thisField.content == field.e) GetComponent<Renderer>().materials[0].color = Color.white;
                    break;
                }
            case "endGame":
                {

                    break;
                }
        }
    }

    private void Change(field _c)
    {
        switch (_c)
        {
            case field.e:
                {
                    thisField.content = field.e;
                    GetComponent<Renderer>().materials[0].color = Color.white;
                    break;
                }
            case field.X:
                {
                    thisField.content = field.X;
                    GetComponent<Renderer>().materials[0].color = Color.red;
                    break;
                }
            case field.O:
                {
                    thisField.content = field.O;
                    GetComponent<Renderer>().materials[0].color = Color.blue;
                    break;
                }
        }
    }

    public void UpdateField(Field f)
    {
        thisField.content = f.content;
        Change(thisField.content);
    }

    public Field GetField()
    {
        return thisField;
    }

    public void Reset()
    {
        Change("empty");
    }
}




