using System;
using System.Collections.Generic;
using System.Text;


class MapStatus
{
    private int index;
    public int status;
    private int choosenMap;
    private Boolean toPlay;
    private static MapStatus mapStat = new MapStatus();  
    private const int MAP_1 = 4;
    private const int MAP_2 = 2;
    private const int MAP_3 = 1;

    public MapStatus() {
        this.index = 0;
        this.status = 0;
        this.choosenMap = 0;
    }

    public int getIndex() { return index; }
    public int getStatus() { return status; }
    public int getChoosenMap() { return choosenMap; }
    public Boolean getToPlay() { return toPlay; }
    public void setToPlay(Boolean toPlay) { this.toPlay = toPlay; }
    public void setChoosenMap(int choosenMap) { this.choosenMap = choosenMap; }
    public void setIndex(int index) { this.index = index; }
    public void setStatus(int status) {
        if (status == 1)
        {
            //Console.WriteLine(this.status +" -- " + MAP_1);
            this.status = this.status | MAP_1;
            //Console.WriteLine("After : "+this.status + " -- " + MAP_1);
        }
        if(status == 2) this.status = this.status | MAP_2;
        if(status == 3) this.status = this.status | MAP_3;
    }

    public void setAllStatus(int status)
    {
        this.status = this.status | status;
    }

    /// <summary>
    /// return true if index available
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Boolean isAvailable(int index)
    {
        int temp = 0;
        if (index < 1 || index > 3) return false;
        if (index == 1) temp = MAP_1;
        if (index == 2) temp = MAP_2;
        if (index == 3) temp = MAP_3;

        //int temp = this.status & status;
        if ((this.status & temp).Equals(temp)) return true;
        return false;
    }

    public static MapStatus getInstance()
    {
        return mapStat;
    }

}

