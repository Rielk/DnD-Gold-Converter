using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DnD_Gold_Converter
{
	internal class WalletVM : INotifyPropertyChanged
	{
		private bool useElectrum = false;
		public bool UseElectrum { get => useElectrum; set => SetFieldCur(ref useElectrum, value); }

		private int? curPlat = 0;
		private int? curGold = 0;
		private int? curElec = 0;
		private int? curSilv = 0;
		private int? curCopp = 0;
		private int totPlat = 0;
		private int totGold = 0;
		private int totElec = 0;
		private int totSilv = 0;
		private int totCopp = 0;
		private int ranPlat = 0;
		private int ranGold = 0;
		private int ranElec = 0;
		private int ranCopp = 0;
		private int ranSilv = 0;

		public int? CurPlat { get => curPlat; set => SetFieldCur(ref curPlat, value); }
		public int? CurGold { get => curGold; set => SetFieldCur(ref curGold, value); }
		public int? CurElec { get => curElec; set => SetFieldCur(ref curElec, value); }
		public int? CurSilv { get => curSilv; set => SetFieldCur(ref curSilv, value); }
		public int? CurCopp { get => curCopp; set => SetFieldCur(ref curCopp, value); }

		public int TotPlat { get => totPlat; set => SetField(ref totPlat, value); }
		public int TotGold { get => totGold; set => SetField(ref totGold, value); }
		public int TotElec { get => totElec; set => SetField(ref totElec, value); }
		public int TotSilv { get => totSilv; set => SetField(ref totSilv, value); }
		public int TotCopp { get => totCopp; set => SetField(ref totCopp, value); }

		public int RanPlat { get => ranPlat; set => SetField(ref ranPlat, value); }
		public int RanGold { get => ranGold; set => SetField(ref ranGold, value); }
		public int RanElec { get => ranElec; set => SetField(ref ranElec, value); }
		public int RanSilv { get => ranSilv; set => SetField(ref ranSilv, value); }
		public int RanCopp { get => ranCopp; set => SetField(ref ranCopp, value); }

		private ICommand randomizeCommand;
		public ICommand RandomizeCommand => randomizeCommand ?? (randomizeCommand = new Command(Randomize, () => true));

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		protected bool SetFieldCur<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (SetField(ref field, value, propertyName))
			{ UpdateTotals(); return true; }
			else { return false; }
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		private void UpdateTotals()
		{
			int sumAll = (CurCopp ?? 0) + (10 * CurSilv ?? 0) + (50 * CurElec ?? 0) + (100 * CurGold ?? 0) + (1000 * CurPlat ?? 0);
			sumAll -= 1000 * (TotPlat = sumAll / 1000);
			sumAll -= 100 * (TotGold = sumAll / 100);
			if (UseElectrum) sumAll -= 50 * (TotElec = sumAll / 50);
			else TotElec = 0;
			sumAll -= 10 * (TotSilv = sumAll / 10);
			TotCopp = sumAll;

			Randomize();
		}

		private void Randomize()
		{
			var random = new Random();

			int plat = TotPlat, gold = TotGold, elec = TotElec, silv = TotSilv, copp = TotCopp;

			int plat_change = plat - random.Next(plat / 4, plat / 2 + 1);
			RanPlat = plat - plat_change;
			gold += 10 * plat_change;

			int gold_change = random.Next(Math.Min(gold / 10, 14), Math.Min(gold + 1, 15));
			RanGold = gold - gold_change;
			elec += 2 * gold_change;

			int elec_change = random.Next(Math.Min(elec / 2, 19), Math.Min(elec + 1, 20));
			RanElec = elec - elec_change;
			silv += 5 * elec_change;

			int silv_change = random.Next(Math.Min(silv / 10, 9), Math.Min(silv + 1, 10));
			RanSilv = silv - silv_change;
			RanCopp = copp + 10 * silv_change;
		}
	}
}
