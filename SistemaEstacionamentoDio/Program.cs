using System;
using System.Collections.Generic;

namespace SistemaEstacionamentoDio
{

    class Program
    {
        static void Main()
        {
            Estacionamento estacionamento = new Estacionamento();

            while (true)
            {
                Console.WriteLine("Sistema de Gerenciamento de Estacionamento");
                Console.WriteLine("1. Adicionar Veículo");
                Console.WriteLine("2. Remover Veículo");
                Console.WriteLine("3. Listar Veículos Estacionados");
                Console.WriteLine("4. Sair");

                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        AdicionarVeiculo(estacionamento);
                        break;
                    case "2":
                        RemoverVeiculo(estacionamento);
                        break;
                    case "3":
                        ListarVeiculos(estacionamento);
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void AdicionarVeiculo(Estacionamento estacionamento)
        {
            Console.WriteLine("Informe a placa do veículo:");
            string placa = Console.ReadLine();
            Console.WriteLine("Informe o tipo do veículo:");
            string tipo = Console.ReadLine();

            Veiculo veiculo = new Veiculo(placa, tipo);
            estacionamento.AdicionarVeiculo(veiculo);

            Console.WriteLine($"Veículo {tipo} com placa {placa} foi adicionado ao estacionamento.");
        }

        static void RemoverVeiculo(Estacionamento estacionamento)
        {
            Console.WriteLine("Informe a placa do veículo a ser removido:");
            string placa = Console.ReadLine();

            bool removido = estacionamento.RemoverVeiculo(placa);

            if (removido)
            {
                Console.WriteLine($"Veículo com placa {placa} foi removido do estacionamento.");
            }
            else
            {
                Console.WriteLine("Veículo não encontrado no estacionamento.");
            }
        }

        static void ListarVeiculos(Estacionamento estacionamento)
        {
            Console.WriteLine("Veículos estacionados:");
            List<Veiculo> veiculos = estacionamento.ListarVeiculos();

            foreach (var veiculo in veiculos)
            {
                Console.WriteLine($"Placa: {veiculo.Placa}, Tipo: {veiculo.Tipo}, Hora de entrada: {veiculo.HoraEntrada}, Hora de saída: {veiculo.HoraSaida}, Valor Cobrado: R$ {veiculo.CalcularValorCobrado():F2}");
            }
        }
    }

    class Veiculo
    {
        public string Placa { get; private set; }
        public string Tipo { get; private set; }
        public DateTime HoraEntrada { get; private set; }
        public DateTime HoraSaida { get; private set; }

        public Veiculo(string placa, string tipo)
        {
            Placa = placa;
            Tipo = tipo;
            HoraEntrada = DateTime.Now;
        }

        public void RegistrarSaida()
        {
            HoraSaida = DateTime.Now;
        }

        public decimal CalcularValorCobrado()
        {
            // Cálculo do valor: R$ 5 por hora
            TimeSpan tempoEstacionado = HoraSaida - HoraEntrada;
            decimal valorCobrado = (decimal)tempoEstacionado.TotalHours * 5.0m; // R$ 5 por hora
            return valorCobrado;
        }
    }

    class Estacionamento
    {
        private List<Veiculo> veiculosEstacionados = new List<Veiculo>();

        public void AdicionarVeiculo(Veiculo veiculo)
        {
            veiculosEstacionados.Add(veiculo);
        }

        public bool RemoverVeiculo(string placa)
        {
            Veiculo veiculo = veiculosEstacionados.Find(v => v.Placa == placa);

            if (veiculo != null)
            {
                veiculo.RegistrarSaida();
                veiculosEstacionados.Remove(veiculo);
                return true;
            }

            return false;
        }

        public List<Veiculo> ListarVeiculos()
        {
            return veiculosEstacionados;
        }
    }

}
