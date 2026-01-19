using System.Collections;
using System.Text;
using _Project.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Inputs
{
    public class InputStringReader
    {
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly StringBuilder _buffer = new StringBuilder();
        private bool _isActive;

        public InputStringReader(ICoroutinesPerformer coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
        }

        public string CurrentInput => _buffer.ToString();

        public IEnumerator StartProcess(int maxLength)
        {
            _isActive = true;
            _buffer.Clear();

            Debug.Log("Ввeдите символы. Для подтверждения нажмиие Enter");

            yield return _coroutinesPerformer.StartPerform(InputProcess(maxLength));
        }

        private IEnumerator InputProcess(int maxLength)
        {
            while (_isActive)
            {
                string input = Input.inputString;

                if (string.IsNullOrEmpty(input) == false)
                {
                    foreach (char c in input)
                    {
                        if (_buffer.Length < maxLength)
                        {
                            if (char.IsLetterOrDigit(c))
                            {
                                _buffer.Append(c);
                                Debug.Log(_buffer);
                            }
                        }

                        if (c == '\n' || c == '\r')
                        {
                            Submit();
                            break;
                        }
                    }
                }

                yield return new WaitUntil(() => Input.anyKeyDown);
            }
        }

        private void Submit()
        {
            _isActive = false;
            Debug.Log($"Ввод завершён: {_buffer}");
        }
    }
}
