using cky.Reuseables.Singleton;
using cky.Reuseables.Helpers;
using cky.Reuseables.Level;
using UnityEngine;

namespace ConnectFoods.Managers
{
    public class LevelManager : SingletonPersistent<LevelManager>
    {
        [SerializeField] LevelSettings[] levels;
        public LevelSettings LevelSettings;

        int _levelIndex;



        protected override void OnPerAwake()
        {
            _levelIndex = PlayerPrefs.GetInt(PlayerPrefHelper.pPrefsLevelIndex);
            LevelSettings = levels[_levelIndex % levels.Length];
        }



        protected void OnGameSuccess()
        {
            _levelIndex++;
            PlayerPrefs.SetInt(PlayerPrefHelper.pPrefsLevelIndex, _levelIndex);
        }
    }
}