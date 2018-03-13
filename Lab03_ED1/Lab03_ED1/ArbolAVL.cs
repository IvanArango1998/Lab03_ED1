using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab03_ED1
{
    public class NodoArbolAVL <T>
    {
       public T value;
       public int FactorEquilibrio;
       public NodoArbolAVL<T> HijoIzquierdo, HijoDerecho;

        public bool EsHoja
        {
            get
            {
                return HijoDerecho == null && HijoIzquierdo == null;
            }
        }


        public NodoArbolAVL(T value)
        {
            this.value = value;
            this.FactorEquilibrio = 0;
            this.HijoIzquierdo = null;
            this.HijoDerecho = null;
        }
    }
    public class ArbolAVL <T> where T : IComparable
    {

        public NodoArbolAVL<T> Raiz;
        
        public ArbolAVL()
        {
            this.Raiz = null;
        }

        //Buscar
        /// <summary>
        /// Función que recibe un valor, y devuelve un nodo que contenga ese valor
        /// </summary>
        /// <param name="value"> valor a buscar </param>
        /// <param name="raiz"></param>
        /// <returns></returns>
        public NodoArbolAVL<T> Buscar(T value, NodoArbolAVL<T> raiz)
        {
            if (Raiz == null)
            {
                return null;
            }
            else if (raiz.value.CompareTo(value) == 0)
            {
                return raiz;
            }
            else if (raiz.value.CompareTo(value) == -1)
            {
                return Buscar(value, raiz.HijoDerecho);
            }
            else
            {
                return Buscar(value, raiz.HijoIzquierdo);
            }
        }

        //Obtener Factor de Equilibrio
        public int ObtenerFactorEquilibrio(NodoArbolAVL <T> x)
        {
            if (x == null)
            {
                return -1;
            }
            else
               return x.FactorEquilibrio;
        }

        //Rotación Simple Izquierda
        public NodoArbolAVL<T> RotacionIzquierda(NodoArbolAVL<T> Nodo)
        {
            NodoArbolAVL<T> Auxiliar = Nodo.HijoIzquierdo;
            Nodo.HijoIzquierdo = Auxiliar.HijoDerecho;

            Auxiliar.HijoDerecho = Nodo;
            //Obtiene el mayor factor de equilibrio de sus hijos.
            Nodo.FactorEquilibrio = Math.Max(ObtenerFactorEquilibrio(Nodo.HijoIzquierdo), ObtenerFactorEquilibrio(Nodo.HijoDerecho) + 1);
            Auxiliar.FactorEquilibrio = Math.Max(ObtenerFactorEquilibrio(Auxiliar.HijoIzquierdo), ObtenerFactorEquilibrio(Auxiliar.HijoDerecho)) + 1;
            return Auxiliar;
        }
        //Rotación Simple Derecha
        public NodoArbolAVL<T> RotacionDerecha(NodoArbolAVL<T> Nodo)
        {
            NodoArbolAVL<T> Auxiliar = Nodo.HijoDerecho;
            Nodo.HijoDerecho = Auxiliar.HijoIzquierdo;

            Auxiliar.HijoIzquierdo = Nodo;
            //Obtiene el mayor factor de equilibrio de sus hijos.
            Nodo.FactorEquilibrio = Math.Max(ObtenerFactorEquilibrio(Nodo.HijoIzquierdo), ObtenerFactorEquilibrio(Nodo.HijoDerecho)) + 1;
            Auxiliar.FactorEquilibrio = Math.Max(ObtenerFactorEquilibrio(Auxiliar.HijoIzquierdo), ObtenerFactorEquilibrio(Auxiliar.HijoDerecho)) + 1;
            return Auxiliar;
        }

        //Rotación Doble Izquierda
        public NodoArbolAVL<T> RotacionDobleIzquierda(NodoArbolAVL<T> Nodo)
        {
            NodoArbolAVL<T> Auxiliar;
            Nodo.HijoIzquierdo = RotacionDerecha(Nodo.HijoIzquierdo);
            Auxiliar = RotacionIzquierda(Nodo);
            return Auxiliar;
        }
        //Rotación Doble Derecha
        public NodoArbolAVL<T> RotacionDobleDerecha(NodoArbolAVL<T> Nodo)
        {
            NodoArbolAVL<T> Auxiliar;
            Nodo.HijoDerecho = RotacionIzquierda(Nodo.HijoDerecho);
            Auxiliar = RotacionDerecha(Nodo);
            return Auxiliar;
        }
        
        //Metodo InsertarAVL
        public NodoArbolAVL <T> InsertarAVL(NodoArbolAVL <T> Nuevo, NodoArbolAVL<T> SubArbol)
        {
            NodoArbolAVL<T> NuevoPadre = SubArbol;
            if (Nuevo.value.CompareTo(SubArbol.value) == -1)
            {
                if (SubArbol.HijoIzquierdo == null)
                {
                    SubArbol.HijoIzquierdo = Nuevo;
                }
                else
                {
                    SubArbol.HijoIzquierdo = InsertarAVL(Nuevo, SubArbol.HijoIzquierdo);
                    if (ObtenerFactorEquilibrio(SubArbol.HijoIzquierdo) - ObtenerFactorEquilibrio (SubArbol.HijoDerecho) == 2 )
                    {
                        if (Nuevo.value.CompareTo(SubArbol.HijoIzquierdo) == -1)
                        {
                            NuevoPadre = RotacionIzquierda(SubArbol);
                        }
                        else
                        {
                            NuevoPadre = RotacionDobleIzquierda(SubArbol);
                        }
                    }
                }
                

            }
            else if(Nuevo.value.CompareTo(SubArbol.value) == 1)
            {
                if (SubArbol.HijoDerecho == null)
                {
                    SubArbol.HijoDerecho = Nuevo;
                }
                else
                {
                    SubArbol.HijoDerecho = InsertarAVL(Nuevo, SubArbol.HijoDerecho);
                    if (ObtenerFactorEquilibrio(SubArbol.HijoDerecho) - ObtenerFactorEquilibrio(SubArbol.HijoIzquierdo) == 2)
                    {
                        if (Nuevo.value.CompareTo(SubArbol.HijoDerecho) == 1)
                        {
                            NuevoPadre = RotacionDerecha(SubArbol);
                        }
                        else
                        {
                            NuevoPadre = RotacionDobleDerecha(SubArbol);
                        }
                    }
                }
            }
            else
            {
                throw new System.InvalidOperationException("Nodo Duplicado");
            }
            //Actualizando Factor Equilibrio
            if (SubArbol.HijoIzquierdo == null && SubArbol.HijoDerecho != null)
            {
                SubArbol.FactorEquilibrio = SubArbol.HijoDerecho.FactorEquilibrio + 1;
            }
            else if (SubArbol.HijoDerecho == null && SubArbol.HijoIzquierdo != null)
            {
                SubArbol.FactorEquilibrio = SubArbol.HijoIzquierdo.FactorEquilibrio + 1;
            }
            else
            {
                SubArbol.FactorEquilibrio = Math.Max(ObtenerFactorEquilibrio(SubArbol.HijoIzquierdo), ObtenerFactorEquilibrio(SubArbol.HijoDerecho)) +1;
            }
            return NuevoPadre;
        }
        public void Insertar(T value)
        {
            NodoArbolAVL<T> Nuevo = new NodoArbolAVL<T>(value);
            if (Raiz == null)
            {
                Raiz = Nuevo;
            }
            else
            {
                Raiz = InsertarAVL(Nuevo, Raiz);
            }

        }

        #region Eliminación
        ////Eliminar AVL
        //public NodoArbolAVL<T> Eliminar(T valor)
        //{
        //    NodoArbolAVL<T> auxiliar = Raiz;
        //    NodoArbolAVL<T> padre = Raiz;
        //    bool esHijoIzquierdo = true;
        //    while (auxiliar.value.CompareTo(valor) != 0)
        //    {
        //        padre = auxiliar;
        //        if (valor.CompareTo(auxiliar.value) < 0)
        //        {
        //            esHijoIzquierdo = true;
        //            auxiliar = auxiliar.HijoIzquierdo;
        //        }
        //        else
        //        {
        //            esHijoIzquierdo = false;
        //            auxiliar = auxiliar.HijoDerecho;
        //        }
        //        if (auxiliar == null)
        //        {
        //            return null;
        //        }
        //    }// Fin ciclo inicial
        //    if (auxiliar.EsHoja)
        //    {
        //        if (auxiliar == Raiz)
        //        {
        //            Raiz = null;
        //        }
        //        else if (esHijoIzquierdo)
        //        {
        //            padre.HijoIzquierdo = null;
        //        }
        //        else
        //        {
        //            padre.HijoDerecho = null;
        //        }
        //    }
        //    else if (auxiliar.HijoDerecho == null)
        //    {
        //        NodoArbolAVL<T> temp = auxiliar.HijoIzquierdo;
        //        if (auxiliar == Raiz)
        //        {
        //            Raiz = temp;
        //        }
        //        else if (esHijoIzquierdo)
        //        {
        //            padre.HijoIzquierdo = temp;
        //        }
        //        else
        //        {
        //            padre.HijoDerecho = temp;
        //        }
        //    }
        //    else if (auxiliar.HijoIzquierdo == null)
        //    {
        //        NodoArbolAVL<T> temp = auxiliar.HijoDerecho;
        //        if (auxiliar == Raiz)
        //        {
        //            Raiz = temp;
        //        }
        //        else if (esHijoIzquierdo)
        //        {
        //            padre.HijoIzquierdo = temp;
        //        }
        //        else
        //        {
        //            padre.HijoDerecho = temp;
        //        }
        //    }
        //    else
        //    {
        //        NodoArbolAVL<T> reemplazo = Reemplazar(auxiliar);
        //        if (auxiliar == Raiz)
        //        {
        //            Raiz = reemplazo;
        //        }
        //        else if (esHijoIzquierdo)
        //        {
        //            padre.HijoIzquierdo = reemplazo;
        //        }
        //        else
        //        {
        //            padre.HijoDerecho = reemplazo;
        //        }
        //        reemplazo.HijoIzquierdo = auxiliar.HijoIzquierdo;

        //    }
        //    return auxiliar;
        //}
        #endregion

        /// <summary>
        /// Elimina un Nodo mediante sustitucion
        /// </summary>
        /// <param name="NodoAEliminar">Nodo a Eliminar </param>
        /// <returns>Nodo de Reemplazo</returns>
        private NodoArbolAVL<T> Reemplazar(NodoArbolAVL<T> NodoAEliminar)
        {
            NodoArbolAVL<T> remplazoPadre = NodoAEliminar;
            NodoArbolAVL<T> reemplazo = NodoAEliminar;
            NodoArbolAVL<T> auxiliar = NodoAEliminar.HijoDerecho;
            while (auxiliar != null)
            {
                remplazoPadre = reemplazo;
                reemplazo = auxiliar;
                auxiliar = auxiliar.HijoIzquierdo;
            }
            if (reemplazo != NodoAEliminar.HijoDerecho)
            {
                remplazoPadre.HijoIzquierdo = reemplazo.HijoDerecho;
                reemplazo.HijoDerecho = NodoAEliminar.HijoDerecho;
            }
            return reemplazo;
        }


        //Recorridos
        private void PreOrder(NodoArbolAVL<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                Elements.Add(Aux.value);
                PreOrder(Aux.HijoIzquierdo, ref Elements);
                PreOrder(Aux.HijoDerecho, ref Elements);
            }
        }
        /// <summary>
        /// Metodo Recursivo que recorre el arbol
        /// </summary>
        /// <param name="Aux">Nodo Raiz</param>
        /// <param name="Elements">Lista de Datos en Orden</param>
        private void InOrder(NodoArbolAVL<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                InOrder(Aux.HijoIzquierdo, ref Elements);
                Elements.Add(Aux.value);
                InOrder(Aux.HijoDerecho, ref Elements);
            }
        }
        /// <summary>
        /// Metodo Recursivo que recorre el arbol
        /// </summary>
        /// <param name="Aux">Nodo Raiz</param>
        /// <param name="Elements">Lista de Datos en Orden</param>
        private void PostOrder(NodoArbolAVL<T> Aux, ref List<T> Elements)
        {
            if (Aux != null)
            {
                PostOrder(Aux.HijoIzquierdo, ref Elements);
                PostOrder(Aux.HijoDerecho, ref Elements);
                Elements.Add(Aux.value);
            }
        }


    }
}