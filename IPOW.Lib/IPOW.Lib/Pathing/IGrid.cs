using System;

namespace Pathing
{
	public interface IGrid
	{
		bool FieldBlocked(int x, int y);
		int GetGridWidth();
		int GetGridHeight();
	}
}
