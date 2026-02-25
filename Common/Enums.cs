using System;

namespace FTN.Common
{	
	public enum PhaseCode : short
	{
		Unknown = 0x0,
		N = 0x1,
		C = 0x2,
		CN = 0x3,
		B = 0x4,
		BN = 0x5,
		BC = 0x6,
		BCN = 0x7,
		A = 0x8,
		AN = 0x9,
		AC = 0xA,
		ACN = 0xB,
		AB = 0xC,
		ABN = 0xD,
		ABC = 0xE,
		ABCN = 0xF
	}
	
	public enum TransformerFunction : short
	{
		Supply = 1,				// Supply transformer
		Consumer = 2,			// Transformer supplying a consumer
		Grounding = 3,			// Transformer used only for grounding of network neutral
		Voltreg = 4,			// Feeder voltage regulator
		Step = 5,				// Step
		Generator = 6,			// Step-up transformer next to a generator.
		Transmission = 7,		// HV/HV transformer within transmission network.
		Interconnection = 8		// HV/HV transformer linking transmission network with other transmission networks.
	}
	
	public enum WindingConnection : short
	{
		Y = 1,		// Wye
		D = 2,		// Delta
		Z = 3,		// ZigZag
		I = 4,		// Single-phase connection. Phase-to-phase or phase-to-ground is determined by elements' phase attribute.
		Scott = 5,   // Scott T-connection. The primary winding is 2-phase, split in 8.66:1 ratio
		OY = 6,		// 2-phase open wye. Not used in Network Model, only as result of Topology Analysis.
		OD = 7		// 2-phase open delta. Not used in Network Model, only as result of Topology Analysis.
	}

	public enum WindingType : short
	{
		None = 0,
		Primary = 1,
		Secondary = 2,
		Tertiary = 3
	}

	// ========== PROJEKAT 5 - NOVI ENUMI ==========

	/// <summary>
	/// Curve style enumeration - opisuje tip krive
	/// </summary>
	public enum CurveStyle : short
	{
		ConstantYValue = 1,			// Konstantna Y vrednost
		Formula = 2,				// Definisana formulom
		RampYValue = 3,				// Rampa (linearna promena)
		StraightLineYValues = 4		// Prava linija između tačaka
	}

	/// <summary>
	/// Unit multiplier enumeration - množitelji jedinica (SI prefiksi)
	/// </summary>
	public enum UnitMultiplier : short
	{
		None = 0,		// Bez množitelja (1)
		Pico = 1,		// p  (10^-12)
		Nano = 2,		// n  (10^-9)
		Micro = 3,		// μ  (10^-6)
		Milli = 4,		// m  (10^-3)
		Centi = 5,		// c  (10^-2)
		Deci = 6,		// d  (10^-1)
		Kilo = 7,		// k  (10^3)
		Mega = 8,		// M  (10^6)
		Giga = 9,		// G  (10^9)
		Tera = 10		// T  (10^12)
	}

	/// <summary>
	/// Unit symbol enumeration - jedinice mere
	/// </summary>
	public enum UnitSymbol : short
	{
		None = 0,		// Bez jedinice
		// Osnove SI jedinice
		A = 1,			// Amper (struja)
		V = 2,			// Volt (napon)
		W = 3,			// Vat (aktivna snaga)
		VAr = 4,		// Volt-Amper reaktivni (reaktivna snaga)
		VA = 5,			// Volt-Amper (prividna snaga)
		Wh = 6,			// Vat-sat (energija)
		VAh = 7,		// Volt-Amper-sat
		VArh = 8,		// Volt-Amper reaktivni-sat
		// Dodatne jedinice
		Hz = 9,			// Herc (frekvencija)
		Ohm = 10,		// Om (otpornost)
		S = 11,			// Simens (provodnost)
		F = 12,			// Farad (kapacitivnost)
		H = 13,			// Henri (induktivnost)
		// Vreme
		Sec = 14,		// Sekunda
		Min = 15,		// Minut
		Hour = 16,		// Sat
		// Geometrija
		M = 17,			// Metar (dužina)
		M2 = 18,		// Kvadratni metar (površina)
		M3 = 19,		// Kubni metar (zapremina)
		// Ostalo
		Deg = 20,		// Stepen (ugao)
		DegC = 21,		// Stepen Celzijusa (temperatura)
		Pa = 22,		// Paskal (pritisak)
		N = 23,			// Njutn (sila)
		J = 24,			// Džul (energija)
		G = 25,			// Gram (masa)
		Rad = 26		// Radijan (ugao)
	}

	/// <summary>
	/// Switch state enumeration - stanje prekidača
	/// </summary>
	public enum SwitchState : short
	{
		Open = 1,		// Otvoren (ne provodi)
		Close = 2		// Zatvoren (provodi)
	}
}
