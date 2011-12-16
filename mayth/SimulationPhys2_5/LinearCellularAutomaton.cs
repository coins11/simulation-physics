using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulationPhys2_5
{
    /// <summary>
    /// Represents a linear cellular automaton.
    /// </summary>
    public class LinearCellularAutomaton
    {
        #region Fields
        readonly ReadOnlyDictionary<Tuple<bool, bool, bool>, bool> _rules;
        readonly byte _ruleNumber;
        readonly int _length;
        readonly double _density;
        readonly System.Collections.ObjectModel.ReadOnlyCollection<bool> _initialState;

        bool[] state;
        int time;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the rule number.
        /// </summary>
        public byte RuleNumber
        {
            get { return _ruleNumber; }
        }

        /// <summary>
        /// Gets the rule as binary representation.
        /// </summary>
        public string RuleString
        {
            get { return Convert.ToString(RuleNumber, 2).PadLeft(8, '0'); }
        }

        /// <summary>
        /// Gets the length of the cells.
        /// </summary>
        public int Length
        {
            get { return _length; }
        }

        /// <summary>
        /// Gets the initial density.
        /// </summary>
        public double Density
        {
            get { return _density; }
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<bool> InitialState
        {
            get { return _initialState; }
        }

        /// <summary>
        /// Gets the rule as dictionary.
        /// </summary>
        public ReadOnlyDictionary<Tuple<bool, bool, bool>, bool> Rules
        {
            get { return _rules; }
        }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public IList<bool> CurrentState
        {
            get { return state.ToList(); }
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        public int CurrentTime
        {
            get { return time; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new cellular automaton with false that has the specified rule.
        /// </summary>
        /// <param name="rule">A rule number</param>
        /// <param name="length">A length of cells</param>
        public LinearCellularAutomaton(byte rule, int length)
        {
            if (length < 3)
                throw new ArgumentOutOfRangeException("Length is must be more than or equal to 3");

            _ruleNumber = rule;
            _length = length;
            _density = 0.0;

            var rules = Convert.ToString(rule, 2).PadLeft(8, '0');
            _rules = new ReadOnlyDictionary<Tuple<bool, bool, bool>, bool>(new Dictionary<Tuple<bool, bool, bool>, bool>()
            {
                {Tuple.Create(true, true, true), rules[0] == '1' ? true : false},
                {Tuple.Create(true, true, false), rules[1] == '1' ? true : false},
                {Tuple.Create(true, false, true), rules[2] == '1' ? true : false},
                {Tuple.Create(true, false, false), rules[3] == '1' ? true : false},
                {Tuple.Create(false, true, true), rules[4] == '1' ? true : false},
                {Tuple.Create(false, true, false), rules[5] == '1' ? true : false},
                {Tuple.Create(false, false, true), rules[6] == '1' ? true : false},
                {Tuple.Create(false, false, false), rules[7] == '1' ? true : false}
            });
            state = Enumerable.Repeat(false, length).ToArray();
            _initialState = new System.Collections.ObjectModel.ReadOnlyCollection<bool>(state);
            time = 0;
        }

        /// <summary>
        /// Initializes a new cellular automaton with the specified initial density that has the specified rule.
        /// </summary>
        /// <param name="rule">A rule number</param>
        /// <param name="length">A length of cells</param>
        /// <param name="density">An initial density</param>
        public LinearCellularAutomaton(byte rule, int length, double density)
            : this(rule, length)
        {
            _density = density;
            foreach (var i in Enumerable.Range(0, length).OrderBy(n => Guid.NewGuid()).Take((int)(length * density)))
                state[i] = true;
            _initialState = new System.Collections.ObjectModel.ReadOnlyCollection<bool>(state);
        }

        /// <summary>
        /// Initializes a new cellular automaton with the specified states that has the specified rule.
        /// </summary>
        /// <param name="rule">A rule number</param>
        /// <param name="length">A length of cells</param>
        /// <param name="initialState">An initial state</param>
        public LinearCellularAutomaton(byte rule, int length, IList<bool> initialState)
            : this(rule, length)
        {
            _density = (double)initialState.Count(b => b) / length;
            state = initialState.ToArray();
            _initialState = new System.Collections.ObjectModel.ReadOnlyCollection<bool>(initialState);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Increase the time.
        /// </summary>
        public void Next()
        {
            bool[] oldStatus = new bool[Length];
            state.CopyTo(oldStatus, 0);

            state[0] = Rules[Tuple.Create(oldStatus[Length - 1], oldStatus[0], oldStatus[1])];
            for (int i = 1; i < Length; i++)
            {
                var next = Tuple.Create(oldStatus[(i - 1) % Length], oldStatus[i % Length], oldStatus[(i + 1) % Length]);
                state[i] = Rules[next];
            }

            time++;
        }

        /// <summary>
        /// Reset the state to the initial state.
        /// </summary>
        public void Reset()
        {
            state = InitialState.ToArray();
            time = 0;
        }
        #endregion
    }
}
