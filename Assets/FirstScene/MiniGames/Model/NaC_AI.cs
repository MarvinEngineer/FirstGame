using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;

public class NaC_AI
{
    private Board board;

    //временный массив для удобства ряда вычислений
    private field[] fs_line;

    private int strategy;
    private Field currentAttack;

    public NaC_AI()
    {
        board = new Board();
        fs_line = new field[9];
    }

    public Field play(int step)
    {
        if (FirstRule()) return currentAttack;
        if (SecondRule()) return currentAttack;
        
        //Выбор стратегии на первом ходе
        if (step == 1)
            if (board[1, 1] == field.X) strategy = 2;
            else strategy = 1;

        if (strategy == 1) currentAttack = FirstStrategy(step);
        else if (strategy == 2) currentAttack = SecondStrategy(step);

        return currentAttack;
    }
    
    //Первое правило. Проверка на возможность победы(два поля из трех заполнены ноликами)
    private bool FirstRule()
    {
        string Attack;

        //Проверка диоганалей
        Attack = CheckDiagonals(field.O);
        if (Attack != "none")
        {
            currentAttack = SearchEmpty(Attack);
            return true;
        }

        //Проверка строк и столбцов
        Attack = CheckRowsAndColumns(field.O);
        if (Attack != "none")
        {
            currentAttack = SearchEmpty(Attack);
            return true;
        }

        return false;
    }
    //Второе правило. Проверка на угрозу(два поля из трех заполнены крестиками)
    private bool SecondRule()
    {
        string Attack;

        //Проверка диоганалей
        Attack = CheckDiagonals(field.X);
        if (Attack != "none")
        {
            currentAttack = SearchEmpty(Attack);
            return true;
        }

        //Проверка строк и столбцов
        Attack = CheckRowsAndColumns(field.X);
        if (Attack != "none")
        {
            currentAttack = SearchEmpty(Attack);
            return true;
        }

        return false;
    }
    //Стратегия №1. Первый ход крестиком не в центр
    private Field FirstStrategy(int step)
    {
        //Заполним временный массив
        FillTempArray();
        //На первом шаге ставим нолик в центр
        if (step == 1)
        {
            return new Field(1, 1, field.O);
        }
        else if (step == 2)
        {
            List<Field> tmp_list = new List<Field>();
            tmp_list.Add(RoundCheck(new int[] { 2, 5 }, 3));
            tmp_list.Add(RoundCheck(new int[] { 2, 7 }, 1));
            tmp_list.Add(RoundCheck(new int[] { 2, 8 }, 1));
            tmp_list.Add(RoundCheck(new int[] { 2, 4 }, 3));

            foreach (Field _f in tmp_list)
                if (_f != null) return _f;
            return SearchEmpty("random");
        }
        else return SearchEmpty("random");
    }
    //Стратегия №2. Первый ход крестиком в центр
    private Field SecondStrategy(int step)
    {
        FillTempArray();
        if (step == 1)
        {
            return new Field((Random.Range(1, 4)) * 2 - 1, field.O);
        }
        else if (step == 2)
        {
            if (fs_line[sumRound(currentAttack.GetLineInt(), 4)] == field.X)
                return new Field(subRound(currentAttack.GetLineInt(),2), field.O);
            return SearchEmpty("random");
        }
        else return SearchEmpty("random");
    }
    //Функция заполнения временного массива
    private void FillTempArray()
    {
        fs_line[1] = board[0, 0];
        fs_line[2] = board[0, 1];
        fs_line[3] = board[0, 2];
        fs_line[4] = board[1, 2];
        fs_line[5] = board[2, 2];
        fs_line[6] = board[2, 1];
        fs_line[7] = board[2, 0];
        fs_line[8] = board[1, 0];
        fs_line[0] = board[1, 1];
    }        
    //Проверяем по кругу зеркальные решения, если нет вариантов, то возвращает пустой список
    private Field RoundCheck(int[] checkList, int attack)
    {
        List<int> absolute = new List<int>();
        List<int> relative = new List<int>();
        
        for (int i = 1; i < checkList.Length; i++)
            relative.Add(checkList[i] - checkList[0]);

        for (int i = 0; i < 4; i++)
        {
            int j = sumRound(checkList[0], i * 2);
            if (fs_line[j] == field.X)
            {
                absolute.Add(j);
                foreach (int k in relative)
                {
                    int l = sumRound(j, k);
                    if (fs_line[l] == field.X)
                        absolute.Add(l);
                }
            }
            if (absolute.Count == checkList.Length)
                return new Field(sumRound(attack, i*2), field.O);            
            else absolute.Clear();
        }

        return null;
    }
    //Суммирование по кругу в девятиричной системе без нулей
    private int sumRound(int a, int b)
    {
        if ((a + b) > 8) return a + b - 8;
        else return a + b;
    }
    //Вычитание по кругу в девятиричной системе без нулей
    private int subRound(int a, int b)
    {
        if ((a - b) < 1) return a - b + 8;
        else return a - b;
    }
    //Проверка диагоналей на даличие опасных ситуаций
    private string CheckDiagonals(field _s)
    {
        int up = 0;
        int down = 0;

        int edown = 0;
        int eup = 0;

        for (int i = 0; i < 3; i++)
        {
            if (board[i, i] == _s) down++;
            if (board[i, i] == field.e) edown++;

            if (board[2 - i, i] == _s) up++;
            if (board[2 - i, i] == field.e) eup++;
        }
        if ((down == 2) && (edown == 1)) return "down";
        if ((up == 2) && (eup == 1)) return "up";
        return "none";
    }
    //Проверка строк на даличие опасных ситуаций
    private string CheckRowsAndColumns(field _s)
    {
        for (int i = 0; i < 3; i++)
        {
            if (CheckRow(_s, i)) return "row" + i;
            if (CheckColumn(_s, i)) return "column" + i;
        }
        return "none";
    }
    // Проверка строки на даличие опасных ситуаций
    private bool CheckRow(field _s, int _i)
    {
        int r = 0;
        int er = 0;

        for (int i = 0; i < 3; i++)
        {
            if (board[_i, i] == _s) r++;
            if (board[_i, i] == field.e) er++;
        }

        if ((r == 2) && (er == 1)) return true;

        return false;
    }
    //Проверка столбца на даличие опасных ситуаций
    private bool CheckColumn(field _s, int _j)
    {
        int c = 0;
        int ec = 0;

        for (int i = 0; i < 3; i++)
        {
            if (board[i, _j] == _s) c++;
            if (board[i, _j] == field.e) ec++;
        }

        if ((c == 2) && (ec == 1)) return true;

        return false;
    }
    //Поиск свободной ячейки в линии
    private Field SearchEmpty(string _s)
    {
        string _c;
        Field rtn = null;
        //Отрезаем номера строк, столбцов
        if (_s.IndexOf("row") != -1) _c = "row";
        else if (_s.IndexOf("column") != -1) _c = "column";
        else _c = _s;

        switch (_c)
        {
            case "down":
                {
                    for (int i = 0; i < 3; i++)
                        if (board[i, i] == field.e)
                            rtn = new Field(i, i, field.O);
                    break;
                }
            case "up":
                {
                    for (int i = 0; i < 3; i++)
                        if (board[2 - i, i] == field.e)
                            rtn = new Field(2 - i, i, field.O);
                    break;
                }
            case "row":
                {
                    int k = (int)char.GetNumericValue(_s[3]);
                    for (int i = 0; i < 3; i++)
                        if (board[k, i] == field.e)
                            rtn = new Field(k, i, field.O);
                    break;
                }
            case "column":
                {
                    int k = (int)char.GetNumericValue(_s[6]);
                    for (int i = 0; i < 3; i++)
                        if (board[i, k] == field.e)
                            rtn = new Field(i, k, field.O);
                    break;
                }
            case "all":
                {
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            if (board[i, j] == field.e)
                                rtn = new Field(i, j, field.O);
                    break;
                }
            case "random":
                {
                    List<Field> _l = new List<Field>();
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            if (board[i, j] == field.e) _l.Add(new Field(i, j, field.O));
                    if (_l.Count != 0) rtn = _l[Random.Range(0, _l.Count)];
                    break;
                }
            case "default":
                {
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            if (board[i, j] == field.e) rtn = new Field(i, j, field.O);
                    break;
                }
        }
        return rtn;
    }
    //Обновление поля ИИ
    public void UpdateBoard(Board _board)
    {
        board = _board;
    }
        
}

