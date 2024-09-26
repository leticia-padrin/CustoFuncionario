namespace CustoFuncionarioApp.Models
{
    public class Custo
    {
        public decimal SalarioBruto { get; set; }
        public decimal PlanoSaude { get; set; }
        public decimal SeguroVida { get; set; }
        public decimal OutrosBeneficios { get; set; }

        public decimal getINSS_Aliquota()
        {
            if (SalarioBruto <= 1412.00M) return 0.075M;
            if (SalarioBruto <= 2666.68M) return 0.09M;
            if (SalarioBruto <= 4000.03M) return 0.12M;
            if (SalarioBruto <= 7786.02M) return 0.14M;
            return 0M;
        }


        public decimal getINSS_Valor()
        {
            double aliquota = (double)this.getINSS_Aliquota();
            return this.SalarioBruto * Convert.ToDecimal(aliquota);
        }

        public decimal getFGTS()
        {
            return this.SalarioBruto * 0.08M;
        }

        public decimal get13oSalario()
        {
            return this.SalarioBruto;
        }

        public decimal getFerias()
        {
            return this.SalarioBruto + (this.SalarioBruto / 3);
        }

        public decimal getPercentualDespesa(decimal valorDespesa)
        {
            return (valorDespesa / this.getCustoTotal()) * 100;
        }

        public decimal getCustoTotal()
        {
            return this.SalarioBruto +
                   this.getINSS_Valor() +
                   this.getFGTS() +
                   this.get13oSalario() +
                   this.getFerias() +
                   this.PlanoSaude +
                   this.SeguroVida +
                   this.OutrosBeneficios;
        }
    }
}