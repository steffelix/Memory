using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBImage 
{
    
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public byte[] ImageBlob { get; set; } = null!;
    public bool? IsBack { get; set; }

}
