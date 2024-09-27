using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelGiocoHanoi
{
    public class CCalcolatore
    {
        int numeroDischi;
        int[] colonne;
        int passo;
        bool onlyOnce;
        public bool canMove;

        public CCalcolatore(int numeroDischi) 
        {
            this.numeroDischi = numeroDischi;
            colonne = new int[3];
            colonne[0] = numeroDischi;
            passo = 1;
            onlyOnce = false;
            canMove = true;
        }

        public void AvviaGioco() 
        {

        }

        private void risolvi(int n) 
        {
            if (!onlyOnce)
            {
                colonne[0] = (int)Math.Pow(2, n) - 1;
                onlyOnce = true;
            }

            if (canMove) 
            {
                //parte iterativa

                int posDiscoPiccolo = 0;

                for (int i = 0; i < 3; i++)
                {
                    if (colonne[i] % 2 == 1)
                        posDiscoPiccolo = i;
                }

                if (n % 2 == 0)
                {
                    switch (passo)
                    {
                        case 1: //muove il disco più piccolo a dx
                            calcolaMossa(posDiscoPiccolo, posDiscoPiccolo + 1);
                            break;
                        case 2:
                            calcolaMossa(posDiscoPiccolo + 1, posDiscoPiccolo - 1);
                            break;
                    }
                }
                else
                {
                    switch (passo)
                    {
                        case 1: //muove il disco più piccolo a sx
                            calcolaMossa(posDiscoPiccolo, posDiscoPiccolo - 1);
                            break;
                        case 2:
                            calcolaMossa(posDiscoPiccolo + 1, posDiscoPiccolo - 1);
                            break;
                    }
                }

                if (colonne[2] == (int)Math.Pow(2, n) - 1)
                    return;

                if (passo == 2)
                    passo = 0;

                passo++;

                canMove = false;
            }

            risolvi(n);
        }

        private void calcolaMossa(int p, int f) 
        {
            //si pensi all'array come se le due estremità fossero connesse
            if (f > 2)
                f = 0;
            else if (f < 0)
                f = 2;
            if (p > 2)
                p = 0;
            else if (p < 0)
                p = 2;

            int vPezzo = colonne[p];
            int vPezzof = colonne[f];
            int esponente;

            if (vPezzo != 0) //se la partenza è colonna vuota la mossa inversa è garantita
            {
                esponente = (int)Math.Truncate(Math.Log2(colonne[p]));

                while ((int)Math.Pow(2, esponente) != vPezzo) //calcolo del valore del primo pezzo
                {
                    if ((int)Math.Pow(2, esponente) <= vPezzo)
                    {
                        vPezzo -= (int)Math.Pow(2, esponente);
                    }
                    esponente--;
                }

                if (vPezzof != 0)
                {
                    esponente = (int)Math.Truncate(Math.Log2(colonne[f]));

                    while ((int)Math.Pow(2, esponente) != vPezzof) //calcolo del valore del secondo pezzo
                    {
                        if ((int)Math.Pow(2, esponente) <= vPezzof)
                        {
                            vPezzof -= (int)Math.Pow(2, esponente);
                        }
                        esponente--;
                    }
                }

                if ((vPezzo < vPezzof || vPezzof == 0))
                {
                    colonne[p] -= vPezzo;
                    colonne[f] += vPezzo;
                    printMossa(p, f);
                    return;
                }
            }

            vPezzo = colonne[f];
            esponente = (int)Math.Truncate(Math.Log2(colonne[f]));

            while ((int)Math.Pow(2, esponente) != vPezzo) //calcolo del valore del primo pezzo
            {
                if ((int)Math.Pow(2, esponente) <= vPezzo)
                {
                    vPezzo -= (int)Math.Pow(2, esponente);
                }
                esponente--;
            }
            colonne[f] -= vPezzo;
            colonne[p] += vPezzo;
            printMossa(f, p);
        }

        private void printMossa(int p, int f)
        {
            char lettera1 = 'E', lettera2 = 'E';

            switch (p)
            {
                case 0:
                    lettera1 = 'A';
                    break;
                case 1:
                    lettera1 = 'B';
                    break;
                case 2:
                    lettera1 = 'C';
                    break;
            }

            switch (f)
            {
                case 0:
                    lettera2 = 'A';
                    break;
                case 1:
                    lettera2 = 'B';
                    break;
                case 2:
                    lettera2 = 'C';
                    break;
            }
            Console.WriteLine($"Mossa accuratamente calcolata: {lettera1} - {lettera2}");
        }
    }
}
