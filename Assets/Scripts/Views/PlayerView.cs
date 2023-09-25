using Memory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Memory.views
{

    class PlayerView : ViewBaseClass<Player>
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;
        [SerializeField] private Text _elapsed;
        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Model.Name)))
                _name.text = "Name: " + Model.Name;
            if (e.PropertyName.Equals(nameof(Model.Score)))
                _score.text = "Score: " + Model.Score;
            if (e.PropertyName.Equals(nameof(Model.IsActive)))
            {
                if (Model.IsActive)
                {
                    _name.color = Color.red;
                }
                else
                {
                    _name.color = Color.grey;
                }
            }
            if (e.PropertyName.Equals(nameof(Model.Elapsed)))
                _elapsed.text = $"Time: {Model.mm}:{Model.ss}:{Model.ms}";
        }

    }

}