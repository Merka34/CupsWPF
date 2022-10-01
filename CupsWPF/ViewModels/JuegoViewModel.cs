using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CupsWPF.ViewModels
{
    public class JuegoViewModel : INotifyPropertyChanged
    {
        public ICommand IniciarCommand { get; set; }
        public ICommand SeleccionTubo1Command { get; set; }
        public ICommand SeleccionTubo2Command { get; set; }
        public ICommand SeleccionTubo3Command { get; set; }
        public ICommand SeleccionTubo4Command { get; set; }
        public ICommand SeleccionTubo5Command { get; set; }
        public ICommand SeleccionTubo6Command { get; set; }
        public ICommand SeleccionTubo7Command { get; set; }
        public ICommand SeleccionTubo8Command { get; set; }

        private bool _haGanado = true;

        public bool HaGanado
        {
            get { return _haGanado; }
            set { _haGanado = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HaGanado))); }
        }


        int[] colores = { 0, 1, 2, 3, 4, 5, 6 }; // 0 = vacio, 1= rojo, 2=azul, 3=verde, 4=amarillo, 5=morado, 6=rosa
        public ObservableCollection<bool> tuboSeleccionado = new ObservableCollection<bool>{ false, false, false, false, false, false, false, false};
        List<int> generacionColores = new List<int>();
        List<int> generacionColoresRevuelto = new List<int>();

        public ObservableCollection<int> tubo1 = new ObservableCollection<int>{ 0, 0, 0, 0 };
        public ObservableCollection<int> tubo2 = new ObservableCollection<int>{ 0, 0, 0, 0 };
        public ObservableCollection<int> tubo3 = new ObservableCollection<int>{ 0, 0, 0, 0 };
        public ObservableCollection<int> tubo4 = new ObservableCollection<int>{ 0, 0, 0, 0 };
        public ObservableCollection<int> tubo5 = new ObservableCollection<int>{ 0, 0, 0, 0 };
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
            set { tubo1 = value; 
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


        public void Inicio()
        {
            HaGanado = false;
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
                if (i<4)
                {
                    tubo1[i] = generacionColoresRevuelto[i];
                }
                else if (i < 8)
                {
                    tubo2[i-4] = generacionColoresRevuelto[i];
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
            while (generacionColores.Count != 0)
            {
                if (generacionColores.Count != 1)
                    escogidoIndex = r.Next(0, generacionColores.Count);
                else
                    escogidoIndex = 0;

                generacionColoresRevuelto.Add(generacionColores[escogidoIndex]);
                generacionColores.RemoveAt(escogidoIndex);
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
                if (index==indice) //deseleccionar tubo
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
                        if (tuboSeleccion[0] == tuboTransferir[0] || tuboTransferir[0]==0) 
                        {
                            //Encontrar el valor mas pequeño, la cantidad de color del tubo seleccionado
                            //o el espacio disponible del tubo tranferir
                            //Si la cantidad de color del tubo seleccionado es menor o igual a la cantidad de espacio libre
                            if(tuboSeleccion[1] <= tuboTransferir[2])
                            {
                                //Obtiene el objeto tubo del tubo seleccionado
                                int[] tuboSelec = ObtenerObjetoTuboById(index);

                                //Obtiene el objeto tubo del tubo a transferir
                                int[] tuboTrans = ObtenerObjetoTuboById(indice);

                                //Agarra el item saltandose los vacios
                                int f = tuboSelec.Length - tuboSeleccion[2];

                                //Coloca el index a colocar los colores
                                int j = 4 - tuboTransferir[2];

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

                                if (VerificarGanada())
                                {
                                    HaGanado = true;
                                    MessageBox.Show("Has ganado el juego, presiona Nuevo Juego para " +'\n'+"jugar otro juego", "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                            //Si la cantidad de color del tubo es mayor a la cantidad de espacio del tubo libre
                            if (tuboSeleccion[1] > tuboTransferir[2])
                            {
                                int[] tuboSelec = ObtenerObjetoTuboById(index);
                                int[] tuboTrans = ObtenerObjetoTuboById(indice);

                                int f = tuboSelec.Length - tuboSeleccion[2] ;

                                int j = 4 - tuboTransferir[2];
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

                                if (VerificarGanada())
                                {
                                    HaGanado = true;
                                    MessageBox.Show("Has ganado", "¡Felicidades!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }


                    //ConsoleLogListaArray(tuboSeleccionado);
                    //ConsoleLogListaArray(tuboTransferir);
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
                if(i==0)
                    idColor=Tubo1[i];

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
            for (int i = miTubo.Count()-1; i>=0; i--)
            {
                if (miTubo[i]==0)
                {
                    resultado[2]++;
                }
                else if(!yaEncontroColor)
                {
                    yaEncontroColor = true;
                    resultado[0] = miTubo[i];
                    resultado[1]++;
                }
                else if (yaEncontroColor && miTubo[i] == resultado[0])
                {
                    resultado[1]++;
                }
                else if(yaEncontroColor && miTubo[i] != resultado[0])
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

        public JuegoViewModel()
        {
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
}
