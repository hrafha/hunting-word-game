using UnityEngine;

namespace Data.User
{
    public class PlayerDataViewer : MonoBehaviour
    {
#if UNITY_EDITOR

        public PlayerData playerData;
        private bool hasBindedEvents = false;


        private void Start()
        {
            BindData(PlayerManager.Player);
        }


        private void OnDestroy()
        {
            UnbindData(playerData);
        }


        internal void BindData(PlayerData data, bool bindEvents = true)
        {
            UnbindData(playerData);
            playerData = data;
        }


        internal void UnbindData(PlayerData data)
        {
            if (data == null)
                return;

            if (hasBindedEvents)
                hasBindedEvents = false;

            playerData = null;
        }
#endif
    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PlayerDataViewer))]
    public class LevelScriptEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Force Load Data"))
            {
                PlayerDataViewer myTarget = (PlayerDataViewer)target;

                PlayerData data = PlayerManager.Player;
                if (data == null) data = PlayerData.Load();

                myTarget.BindData(data, false);
            }

            if (GUILayout.Button("Reset Player"))
            {
                PlayerData dummy = PlayerData.GetDummy();
                PlayerData.Save(dummy);

                if (PlayerManager.Player != null)
                    PlayerManager.Reload();

                PlayerDataViewer myTarget = (PlayerDataViewer)target;
                myTarget.BindData(dummy);
            }

            if (GUILayout.Button("Save Changes"))
            {
                PlayerDataViewer myTarget = (PlayerDataViewer)target;
                PlayerData.Save(myTarget.playerData);
            }
        }
    }
#endif
}
