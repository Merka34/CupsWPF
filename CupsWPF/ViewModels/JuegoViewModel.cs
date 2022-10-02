using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CupsWPF.ViewModels
{
    public class JuegoViewModel : INotifyPropertyChanged
    {
        public ICommand IniciarCommand { get; set; }
        public ICommand IACommand { get; set; }
        public ICommand SeleccionTubo1Command { get; set; }
        public ICommand SeleccionTubo2Command { get; set; }
        public ICommand SeleccionTubo3Command { get; set; }
        public ICommand SeleccionTubo4Command { get; set; }
        public ICommand SeleccionTubo5Command { get; set; }
        public ICommand SeleccionTubo6Command { get; set; }
        public ICommand SeleccionTubo7Command { get; set; }
        public ICommand SeleccionTubo8Command { get; set; }

        // Habilita o deshabilita el enable de cada boton seleccionar

        Dispatcher _dis;

        private bool _haGanado = true;
        public bool HaGanado
        {
            get { return _haGanado; }
            set { _haGanado = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HaGanado))); }
        }

        private int _selectMovimiento;

        public int SelectMovimiento
        {
            get { return _selectMovimiento; }
            set { _selectMovimiento = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectMovimiento))); }
        }

        private ObservableCollection<Registro> _registros = new ObservableCollection<Registro>();
        public ObservableCollection<Registro> Registros 
        {
            get { return _registros; }
            set 
            {
                _registros = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Registros)));
            }
        }

        private int _cantidadMovimientos;

        public int CantidadMovimientos
        {
            get { return _cantidadMovimientos; }
            set { _cantidadMovimientos = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CantidadMovimientos))); }
        }



        private bool _iaCorriendo = false;

        public bool IACorriendo
        {
            get { return _iaCorriendo; }
            set { _iaCorriendo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IACorriendo))); }
        }



        int[] colores = { 0, 1, 2, 3, 4, 5, 6 }; // 0 = vacio, 1= rojo, 2=azul, 3=verde, 4=amarillo, 5=morado, 6=rosa

        public ObservableCollection<bool> tuboSeleccionado = new ObservableCollection<bool> { false, false, false, false, false, false, false, false };

        //Genera los colores evitando que queden disparejos 1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,6,6,6,6
        List<int> generacionColores = new List<int>();
        //Se encarga de agarrar la colleccion anterior (generacioColores) y altera el orden de los colores 
        List<int> generacionColoresRevuelto = new List<int>();

        //Declaracion de cada tubo
        public ObservableCollection<int> tubo1 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo2 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo3 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo4 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo5 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo6 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo7 = new ObservableCollection<int> { 0, 0, 0, 0 };
        public ObservableCollection<int> tubo8 = new ObservableCollection<int> { 0, 0, 0, 0 };

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<bool> TuboSeleccionado
        {
            get { return tuboSeleccionado; }
            set { tuboSeleccionado = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TuboSeleccionado))); }
        }
        public ObservableCollection<int> Tubo1
        {
            get { return tubo1; }
            set
            {
                tubo1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo1)));
            }
        }
        public ObservableCollection<int> Tubo2
        {
            get { return tubo2; }
            set
            {
                tubo2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo2)));
            }
        }
        public ObservableCollection<int> Tubo3
        {
            get { return tubo3; }
            set
            {
                tubo3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo3)));
            }
        }
        public ObservableCollection<int> Tubo4
        {
            get { return tubo4; }
            set
            {
                tubo4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo4)));
            }
        }
        public ObservableCollection<int> Tubo5
        {
            get { return tubo5; }
            set
            {
                tubo5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo5)));
            }
        }
        public ObservableCollection<int> Tubo6
        {
            get { return tubo6; }
            set
            {
                tubo6 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo6)));
            }
        }
        public ObservableCollection<int> Tubo7
        {
            get { return tubo7; }
            set
            {
                tubo7 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo7)));
            }
        }
        public ObservableCollection<int> Tubo8
        {
            get { return tubo8; }
            set
            {
                tubo8 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tubo8)));
            }
        }

        //Se invoca cuando se le presiona el boton Iniciar Juego
        public void Inicio()
        {
            Registros.Clear();
            CantidadMovimientos = 0;
            HaGanado = false; //Habilita los enable de los botones Seleccionar
            Tubo1 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo2 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo3 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo4 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo5 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo6 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo7 = new ObservableCollection<int> { 0, 0, 0, 0 };
            Tubo8 = new ObservableCollection<int> { 0, 0, 0, 0 };
            //Genera los colores repidiendo siempre 4 del mismo
            GenerarNumeros();
            //Resultado -> 1,1,1,1,2,2,2,2,3,3,3,3 
            //Revolver los colores 
            for (int i = 0; i < generacionColoresRevuelto.Count; i++)
            {
                if (i < 4)
                {
                    tubo1[i] = generacionColoresRevuelto[i];
                }
                else if (i < 8)
                {
                    tubo2[i - 4] = generacionColoresRevuelto[i];
                }
                else if (i < 12)
                {
                    tubo3[i - 8] = generacionColoresRevuelto[i];
                }
                else if (i < 16)
                {
                    tubo4[i - 12] = generacionColoresRevuelto[i];
                }
                else if (i < 20)
                {
                    tubo5[i - 16] = generacionColoresRevuelto[i];
                }
                else if (i < 24)
                {
                    tubo6[i - 20] = generacionColoresRevuelto[i];
                }
            }
            //ConsoleLogListaArray(Tubo1);
        }

        public void VerificarTubosCompletos()
        {
            int numeroComparador = 0;
            bool[] sonIguales = { false, false, false };

            numeroComparador = generacionColoresRevuelto[0];
            if (numeroComparador == generacionColoresRevuelto[1])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[2])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[3])
                sonIguales[0] = true;
            if (sonIguales[0] && sonIguales[1] && sonIguales[2])
            {
                GenerarNumeros();
            }

            numeroComparador = generacionColoresRevuelto[4];
            if (numeroComparador == generacionColoresRevuelto[5])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[6])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[7])
                sonIguales[0] = true;
            if (sonIguales[0] && sonIguales[1] && sonIguales[2])
            {
                GenerarNumeros();
            }


            numeroComparador = generacionColoresRevuelto[8];
            if (numeroComparador == generacionColoresRevuelto[9])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[10])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[11])
                sonIguales[0] = true;
            if (sonIguales[0] && sonIguales[1] && sonIguales[2])
            {
                GenerarNumeros();
            }

            numeroComparador = generacionColoresRevuelto[12];
            if (numeroComparador == generacionColoresRevuelto[13])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[14])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[15])
                sonIguales[0] = true;
            if (sonIguales[0] && sonIguales[1] && sonIguales[2])
            {
                GenerarNumeros();
            }

            numeroComparador = generacionColoresRevuelto[16];
            if (numeroComparador == generacionColoresRevuelto[17])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[18])
                sonIguales[0] = true;
            if (numeroComparador == generacionColoresRevuelto[19])
                sonIguales[0] = true;
            if (sonIguales[0] && sonIguales[1] && sonIguales[2])
            {
                GenerarNumeros();
            }

        }

        public void Aleatoriedad()
        {
            Random r = new Random();
            int escogidoIndex = 0;
            //1,1,2,3,3,4,7,5
            //24 posiciones
            while (generacionColores.Count != 0)
            {
                if (generacionColores.Count != 1)
                    escogidoIndex = r.Next(0, generacionColores.Count);
                else
                    escogidoIndex = 0;

                generacionColoresRevuelto.Add(generacionColores[escogidoIndex]); //4,2,5,1, 6,3,5,3, 6,3,5,1
                generacionColores.RemoveAt(escogidoIndex);//
            }
            VerificarTubosCompletos();
        }

        public void GenerarNumeros()
        {
            generacionColores.Clear();
            generacionColoresRevuelto.Clear();
            for (int i = 1; i < colores.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    generacionColores.Add(colores[i]);
                }
            }
            //generacionColores = 1,1,1,1,2,2,2,2,3,...
            Aleatoriedad();
        }

        public void ConsoleLogLista(List<int> array)
        {
            string res = "El resultado es: ";
            for (int i = 0; i < array.Count; i++)
            {
                res += " | " + array[i];
            }
            MessageBox.Show(res);
        }

        public void ConsoleLogListaArray(int[] array)
        {
            string res = "El resultado es: ";
            for (int i = 0; i < array.Length; i++)
            {
                res += " | " + array[i];
            }
            MessageBox.Show(res);
        }

        public void EvaluarSeleccion(int indice)
        {
            bool yaSeleccion = false;
            int index = 0;
            for (int i = 0; i < tuboSeleccionado.Count; i++)
            {
                if (tuboSeleccionado[i])
                {
                    yaSeleccion = true;
                    index = i;
                    break;
                }
            }
            if (yaSeleccion)
            {
                if (index == indice) //deseleccionar tubo
                {
                    tuboSeleccionado[indice] = false;
                }
                else //transladar
                {
                    //Verfica si es valido
                    int[] tuboSeleccion = ColorYCantidad(index); // colorID, cantidad, espacio = 1, 2, 1 = 4, 1, 0 
                    int[] tuboTransferir = ColorYCantidad(indice);
                    if (tuboTransferir[2] > 0) //Verifica si el tubo a transferir tiene espacio disponible
                    {
                        //El primer color del tubo a transferir es el mismo color del tubo seleccionado o es transparente
                        if (tuboSeleccion[0] == tuboTransferir[0] || tuboTransferir[0] == 0)
                        {
                            //Encontrar el valor mas pequeño, la cantidad de color del tubo seleccionado
                            //o el espacio disponible del tubo tranferir
                            //Si la cantidad de color del tubo seleccionado es menor o igual a la cantidad de espacio libre
                            if (tuboSeleccion[1] <= tuboTransferir[2])
                            {
                                //Obtiene el objeto tubo del tubo seleccionado
                                int[] tuboSelec = ObtenerObjetoTuboById(index);

                                //Obtiene el objeto tubo del tubo a transferir
                                int[] tuboTrans = ObtenerObjetoTuboById(indice);

                                //Agarra el item saltandose los vacios
                                int f = tuboSelec.Length - tuboSeleccion[2];

                                //Coloca el index a colocar los colores
                                int j = 4 - tuboTransferir[2];

                                Registros.Add(new Registro
                                {
                                    Mensaje = $"Movimiento del tubo {index + 1} al tubo {indice + 1}",
                                    IDColor = ColorYCantidad(index)[0]
                                });

                                SelectMovimiento = Registros.Count - 1;

                                for (int w = 0; w < tuboSeleccion[1]; w++)
                                {
                                    f--;
                                    tuboTrans[j] = tuboSelec[f];
                                    tuboSelec[f] = 0;
                                    j++;
                                }
                                RegistrarMovimiento(tuboSelec, index);
                                RegistrarMovimiento(tuboTrans, indice);
                                tuboSeleccionado[index] = false;
                                CantidadMovimientos++;
                                if (VerificarGanada())
                                {
                                    HaGanado = true;
                                    MessageBox.Show("Has ganado el juego, presiona Nuevo Juego para " + '\n' + "jugar otro juego", "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                            //Si la cantidad de color del tubo es mayor a la cantidad de espacio del tubo libre
                            if (tuboSeleccion[1] > tuboTransferir[2])
                            {
                                int[] tuboSelec = ObtenerObjetoTuboById(index);
                                int[] tuboTrans = ObtenerObjetoTuboById(indice);

                                int f = tuboSelec.Length - tuboSeleccion[2];

                                int j = 4 - tuboTransferir[2];

                                Registros.Add(new Registro
                                {
                                    Mensaje = $"Movimiento del tubo {index + 1} al tubo {indice + 1}",
                                    IDColor = ColorYCantidad(index)[0]
                                });

                                SelectMovimiento = Registros.Count - 1;

                                for (int w = 0; w < tuboTransferir[2]; w++)
                                {
                                    f--;
                                    tuboTrans[j] = tuboSelec[f];
                                    tuboSelec[f] = 0;
                                    j++;
                                }
                                RegistrarMovimiento(tuboSelec, index);
                                RegistrarMovimiento(tuboTrans, indice);
                                tuboSeleccionado[index] = false;
                                CantidadMovimientos++;
                                if (VerificarGanada())
                                {
                                    HaGanado = true;
                                    MessageBox.Show("Has ganado", "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                tuboSeleccionado[indice] = true;
            }
        }

        public bool VerificarGanada()
        {
            int idColor = 0;
            for (int i = 0; i < Tubo1.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo1[i];

                if (Tubo1[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo2.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo2[i];

                if (Tubo2[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo3.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo3[i];

                if (Tubo3[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo4.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo4[i];

                if (Tubo4[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo5.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo5[i];

                if (Tubo5[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo6.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo6[i];

                if (Tubo6[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo7.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo7[i];

                if (Tubo7[i] != idColor)
                    return false;
            }

            for (int i = 0; i < Tubo8.Count(); i++)
            {
                if (i == 0)
                    idColor = Tubo8[i];

                if (Tubo8[i] != idColor)
                    return false;
            }

            return true;
        }

        public void RegistrarMovimiento(int[] tuboArray, int idTubo)
        {
            if (idTubo == 0)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo1[i] = tuboArray[i];
                }
            }
            else if (idTubo == 1)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo2[i] = tuboArray[i];
                }
            }
            else if (idTubo == 2)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo3[i] = tuboArray[i];
                }
            }
            else if (idTubo == 3)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo4[i] = tuboArray[i];
                }
            }
            else if (idTubo == 4)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo5[i] = tuboArray[i];
                }
            }
            else if (idTubo == 5)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo6[i] = tuboArray[i];
                }
            }
            else if (idTubo == 6)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo7[i] = tuboArray[i];
                }
            }
            else if (idTubo == 7)
            {
                for (int i = 0; i < tuboArray.Length; i++)
                {
                    Tubo8[i] = tuboArray[i];
                }
            }
        }

        public int[] ObtenerObjetoTuboById(int idTubo)
        {
            int[] miTubo;
            if (idTubo == 0)
            {
                miTubo = tubo1.ToArray();
            }
            else if (idTubo == 1)
            {
                miTubo = tubo2.ToArray();
            }
            else if (idTubo == 2)
            {
                miTubo = tubo3.ToArray();
            }
            else if (idTubo == 3)
            {
                miTubo = tubo4.ToArray();
            }
            else if (idTubo == 4)
            {
                miTubo = tubo5.ToArray();
            }
            else if (idTubo == 5)
            {
                miTubo = tubo6.ToArray();
            }
            else if (idTubo == 6)
            {
                miTubo = tubo7.ToArray();
            }
            else
            {
                miTubo = tubo8.ToArray();
            }
            return miTubo;
        }


        public int[] ColorYCantidad(int idTubo)
        {
            int[] miTubo;
            int[] resultado = { 0, 0, 0 }; //colorID, cantidad, Cantidadtransparentes
            miTubo = ObtenerObjetoTuboById(idTubo);
            bool yaEncontroColor = false;
            for (int i = miTubo.Count() - 1; i >= 0; i--)
            {
                if (miTubo[i] == 0) // posicion es vacia
                {
                    resultado[2]++;
                }
                //color
                else if (!yaEncontroColor) // no se habia enncontrado un color anteriormente?
                {
                    yaEncontroColor = true;
                    resultado[0] = miTubo[i];
                    resultado[1]++;
                }
                //si enteriormente ya habia encontrado un color
                else if (yaEncontroColor && miTubo[i] == resultado[0])
                {
                    resultado[1]++;
                }
                else if (yaEncontroColor && miTubo[i] != resultado[0])
                {
                    break;
                }
            }
            return resultado;
        }

        public void SeleccionarTubo8()
        {
            EvaluarSeleccion(7);
        }
        public void SeleccionarTubo7()
        {
            EvaluarSeleccion(6);
        }
        public void SeleccionarTubo6()
        {
            EvaluarSeleccion(5);
        }
        public void SeleccionarTubo5()
        {
            EvaluarSeleccion(4);
        }
        public void SeleccionarTubo4()
        {
            EvaluarSeleccion(3);
        }
        public void SeleccionarTubo3()
        {
            EvaluarSeleccion(2);
        }

        public void SeleccionarTubo2()
        {
            EvaluarSeleccion(1);
        }

        public void SeleccionarTubo1()
        {
            EvaluarSeleccion(0);
        }

        //Inteligencia Artificial

        public void runIA()
        {
            IACorriendo = true;
            Task.Run(IA);
            //Checa si existe tubos vacios
        }
        int[,] resultadoTotal = new int[8, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        public void IA()
        {
            while (!VerificarGanada())
            {
                //¿Hay tubos vacios?
                int[] resultado = { 0, 0, 0 }; //colorID, cantidad, Cantidadtransparentes
                resultadoTotal = new int[8, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
                for (int i = 0; i < 8; i++)
                {
                    resultado = ColorYCantidad(i);
                    resultadoTotal[i, 0] = resultado[0];
                    resultadoTotal[i, 1] = resultado[1];
                    resultadoTotal[i, 2] = resultado[2];
                }
                int hayTubosDisponibles = -1;
                for (int i = 0; i < 8; i++)
                {
                    if (resultadoTotal[i, 0] == 0 && resultadoTotal[i, 1] == 0 && resultadoTotal[i, 2] == 4)
                    {
                        hayTubosDisponibles = i;
                        break;
                    }
                }
                if (hayTubosDisponibles != -1)
                {
                    tecnicaTubosVacios(hayTubosDisponibles);
                }
                else
                {
                    tecnicaJuntarTubos();
                }

            }
            IACorriendo = false;
        }

        int[] anteriormov = { 0, 0 }; //idTubo, idColor

        public void tecnicaJuntarTubos()
        {
            bool hecho = false;
            // tuboID, colorID, cantidad, transparente
            bool[] idTubosDisponibles = { false, false, false, false, false, false, false, false };
            int[] colorCantidad = { 0, resultadoTotal[0, 0], resultadoTotal[0, 1], resultadoTotal[0, 2] };
            for (int i = 0; i < 8; i++)
            {
                if (resultadoTotal[i, 1] > /*colorCantidad[2]*/1 && /*resultadoTotal[i, 1] +*/ resultadoTotal[i,1]!=4 && resultadoTotal[i, 0] != 0)
                {
                    idTubosDisponibles[i] = true;
                    colorCantidad[0] = i;
                    colorCantidad[1] = resultadoTotal[i, 0];
                    colorCantidad[2] = resultadoTotal[i, 1];
                    colorCantidad[3] = resultadoTotal[i, 2];
                }
            }
            //Verifica si encuentro un tubo grande
            if (colorCantidad[2] > 1 /*&& colorCantidad[3] + colorCantidad[2] != 4*/)
            {
                idTubosDisponibles[colorCantidad[0]] = false;
                //Verdadero Si hay uno grande, busca uno que haya espacio donde poner
                int[] colorCantidadTuboTransferir = { -1, resultadoTotal[0, 0], resultadoTotal[0, 1], resultadoTotal[0, 2] };
                for (int k = 0; k < idTubosDisponibles.Length; k++)
                {
                    if (hecho)
                    {
                        break;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        //Elige donde el colorID sea igual y que el espacio cabe en el tuboseleccioado
                        if (i != colorCantidad[0] && resultadoTotal[i, 0] == colorCantidad[1] && resultadoTotal[i, 2] <= colorCantidad[2])
                        {
                            colorCantidadTuboTransferir[0] = i;
                            colorCantidadTuboTransferir[1] = resultadoTotal[i, 0];
                            colorCantidadTuboTransferir[2] = resultadoTotal[i, 1];
                            colorCantidadTuboTransferir[3] = resultadoTotal[i, 2];
                        }
                    }
                    if (colorCantidadTuboTransferir[0] != -1 && colorCantidad[3]+1 != colorCantidadTuboTransferir[2] &&
                        anteriormov[0]!= colorCantidadTuboTransferir[0] && anteriormov[1] != colorCantidad[0])
                    {
                        anteriormov = new int[] {colorCantidadTuboTransferir[0], colorCantidad[0]};

                        EvaluarSeleccionAI(colorCantidadTuboTransferir[0], colorCantidad[0]);
                        EvaluarSeleccionAI(colorCantidad[0], colorCantidadTuboTransferir[0]);
                        hecho = true;
                    }
                    else
                    {
                        for (int m = 0; m < idTubosDisponibles.Length; m++)
                        {
                            if (idTubosDisponibles[m])
                            {
                                idTubosDisponibles[m] = false;
                                colorCantidad[0] = m;
                                colorCantidad[1] = resultadoTotal[m, 0];
                                colorCantidad[2] = resultadoTotal[m, 1];
                                colorCantidad[3] = resultadoTotal[m, 2];
                                break;
                            }
                        }
                    }
                }
                
            }
            //Si aun no se ha hecho
            if (!hecho)
            {
                // Busca los tubos que tengan espacio y cual es el primer color que hay
                
                for (int i = 0; i < idTubosDisponibles.Length; i++)
                {
                    if (resultadoTotal[i, 2]>0)
                    {
                        for (int j = 0; j < idTubosDisponibles.Length; j++)
                        {
                            if (i!=j && resultadoTotal[j, 0] == resultadoTotal[i,0]
                                && anteriormov[0]!=j && anteriormov[1]!=i)
                            {
                                anteriormov = new int[] {j, i};
                                EvaluarSeleccionAI(j, i);
                                EvaluarSeleccionAI(i, j);
                            }
                        }
                    }
                }
            }
        }

        public void tecnicaTubosVacios(int idTubo)
        {
            // tuboID, colorID, cantidad, transparente
            int[] colorCantidad = { 0, resultadoTotal[0, 0], resultadoTotal[0, 1], resultadoTotal[0, 2] };
            for (int i = 0; i < 8; i++)
            {
                if (resultadoTotal[i, 1] > colorCantidad[2] && resultadoTotal[i, 0] != 0 
                    && colorCantidad[3] + colorCantidad[2] != 4)
                {
                    colorCantidad[0] = i;
                    colorCantidad[1] = resultadoTotal[i, 0];
                    colorCantidad[2] = resultadoTotal[i, 1];
                    colorCantidad[3] = resultadoTotal[i, 2];
                }
            }
            //Verifica si encuentro un tubo grande
            if (colorCantidad[2] > 1 && colorCantidad[3] + colorCantidad[2] != 4)
            {
                EvaluarSeleccionAI(colorCantidad[0], idTubo);
            }
            else
            {
                //Busca los colores que haya mas
                //int[,] misColores = new int[6, 2] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } };
                List<miColor> misColores = new List<miColor>()
                {
                new miColor{ Cantidad=0, CantidadTotal=0, ColorID=1, Transparente=0, TuboID=0 },
                new miColor{ Cantidad=0, CantidadTotal=0, ColorID=2, Transparente=0, TuboID=0 },
                new miColor{ Cantidad=0, CantidadTotal=0, ColorID=3, Transparente=0, TuboID=0 },
                new miColor{ Cantidad=0, CantidadTotal=0, ColorID=4, Transparente=0, TuboID=0 },
                new miColor{ Cantidad=0, CantidadTotal=0, ColorID=5, Transparente=0, TuboID=0 },
                new miColor{ Cantidad=0, CantidadTotal=0, ColorID=6, Transparente=0, TuboID=0 },
                };
                for (int i = 0; i < 8; i++)
                {
                    switch (resultadoTotal[i, 0])
                    {
                        case 1:
                            misColores[0].CantidadTotal++;
                            break;
                        case 2:
                            misColores[1].CantidadTotal++;
                            break;
                        case 3:
                            misColores[2].CantidadTotal++;
                            break;
                        case 4:
                            misColores[3].CantidadTotal++;
                            break;
                        case 5:
                            misColores[4].CantidadTotal++;
                            break;
                        case 6:
                            misColores[5].CantidadTotal++;
                            break;
                        default:
                            break;
                    }
                }

                List<miColor> misColoresOrdenado = misColores.OrderByDescending(x => x.CantidadTotal).ToList();

                if (misColoresOrdenado[0].CantidadTotal > 1)
                {
                    int cantidadMaxima = misColoresOrdenado[0].CantidadTotal;
                    int colorID = misColoresOrdenado[0].ColorID;
                    bool[] tubos = SearchTuboByColor(colorID);
                    for (int i = 0; i < tubos.Length; i++)
                    {
                        if (tubos[i])
                        {
                            EvaluarSeleccionAI(i, idTubo);
                        }
                    }
                }
            }
        }

        public bool[] SearchTuboByColor(int idColor)
        {
            bool[] idTubo = { false, false, false, false, false, false, false, false };

            for (int i = 0; i < 8; i++)
            {
                int[] miTubo = ObtenerObjetoTuboById(i);
                for (int j = miTubo.Length - 1; j >= 0; j--)
                {
                    if (miTubo[j] == idColor)
                    {
                        idTubo[i] = true;
                        break;
                    }
                    else if (miTubo[j] == 0)
                    {
                        continue;
                    }
                    else if (miTubo[j] != idColor)
                    {
                        break;
                    }
                }
            }

            return idTubo;
        }

        public void EvaluarSeleccionAI(int index, int indice)
        {
            if (index == indice) //deseleccionar tubo
            {
                tuboSeleccionado[indice] = false;
            }
            else //transladar
            {
                
                //Verfica si es valido
                int[] tuboSeleccion = ColorYCantidad(index);
                int[] tuboTransferir = ColorYCantidad(indice);
                if (tuboTransferir[2] > 0) //Verifica si el tubo a transferir tiene espacio disponible
                {
                    //El primer color del tubo a transferir es el mismo color del tubo seleccionado o es transparente
                    if (tuboSeleccion[0] == tuboTransferir[0] || tuboTransferir[0] == 0)
                    {
                        //Encontrar el valor mas pequeño, la cantidad de color del tubo seleccionado
                        //o el espacio disponible del tubo tranferir
                        //Si la cantidad de color del tubo seleccionado es menor o igual a la cantidad de espacio libre
                        if (tuboSeleccion[1] <= tuboTransferir[2])
                        {
                            //Obtiene el objeto tubo del tubo seleccionado
                            int[] tuboSelec = ObtenerObjetoTuboById(index);

                            //Obtiene el objeto tubo del tubo a transferir
                            int[] tuboTrans = ObtenerObjetoTuboById(indice);

                            //Agarra el item saltandose los vacios
                            int f = tuboSelec.Length - tuboSeleccion[2];

                            //Coloca el index a colocar los colores
                            int j = 4 - tuboTransferir[2];
                            _dis.Invoke(() =>
                            {
                                Registros.Add(new Registro
                                {
                                    Mensaje = $"Movimiento del tubo {index + 1} al tubo {indice + 1}",
                                    IDColor = ColorYCantidad(index)[0]
                                });

                                SelectMovimiento = Registros.Count - 1;
                            });

                            for (int w = 0; w < tuboSeleccion[1]; w++)
                            {
                                f--;
                                tuboTrans[j] = tuboSelec[f];
                                tuboSelec[f] = 0;
                                j++;
                            }
                            RegistrarMovimiento(tuboSelec, index);
                            RegistrarMovimiento(tuboTrans, indice);
                            tuboSeleccionado[index] = false;


                            CantidadMovimientos++;
                            Thread.Sleep(1000);
                            if (VerificarGanada())
                            {
                                HaGanado = true;
                                MessageBox.Show("Has ganado el juego, presiona Nuevo Juego para " + '\n' + "jugar otro juego", "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        //Si la cantidad de color del tubo es mayor a la cantidad de espacio del tubo libre
                        if (tuboSeleccion[1] > tuboTransferir[2])
                        {
                            int[] tuboSelec = ObtenerObjetoTuboById(index);
                            int[] tuboTrans = ObtenerObjetoTuboById(indice);

                            int f = tuboSelec.Length - tuboSeleccion[2];

                            int j = 4 - tuboTransferir[2];
                            _dis.Invoke(() =>
                            {
                                Registros.Add(new Registro
                                {
                                    Mensaje = $"Movimiento del tubo {index + 1} al tubo {indice + 1}",
                                    IDColor = ColorYCantidad(index)[0]
                                });
                                SelectMovimiento = Registros.Count - 1;
                            });
                            for (int w = 0; w < tuboTransferir[2]; w++)
                            {
                                f--;
                                tuboTrans[j] = tuboSelec[f];
                                tuboSelec[f] = 0;
                                j++;
                            }
                            RegistrarMovimiento(tuboSelec, index);
                            RegistrarMovimiento(tuboTrans, indice);
                            tuboSeleccionado[index] = false;

                            CantidadMovimientos++;
                            Thread.Sleep(1000);
                            if (VerificarGanada())
                            {
                                HaGanado = true;
                                MessageBox.Show("Has ganado", "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
                //MessageBox.Show($"Movimiento del tubo {index+1} al tubo {indice+1}");
                
                //ConsoleLogListaArray(tuboSeleccionado);
                //ConsoleLogListaArray(tuboTransferir);
            }
        }

        public JuegoViewModel()
        {
            _dis = Dispatcher.CurrentDispatcher;
            IACommand = new RelayCommand(runIA);
            SeleccionTubo1Command = new RelayCommand(SeleccionarTubo1);
            SeleccionTubo2Command = new RelayCommand(SeleccionarTubo2);
            SeleccionTubo3Command = new RelayCommand(SeleccionarTubo3);
            SeleccionTubo4Command = new RelayCommand(SeleccionarTubo4);
            SeleccionTubo5Command = new RelayCommand(SeleccionarTubo5);
            SeleccionTubo6Command = new RelayCommand(SeleccionarTubo6);
            SeleccionTubo7Command = new RelayCommand(SeleccionarTubo7);
            SeleccionTubo8Command = new RelayCommand(SeleccionarTubo8);
            IniciarCommand = new RelayCommand(Inicio);
        }

    }

    public class Registro
    {
        public string Mensaje { get; set; } = "";
        public int IDColor { get; set; }
    }

    class miColor
    {
        public int TuboID { get; set; }
        public int ColorID { get; set; }
        public int Cantidad { get; set; }
        public int CantidadTotal { get; set; }
        public int Transparente { get; set; }
    }
}
