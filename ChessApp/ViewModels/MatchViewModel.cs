using CommunityToolkit.Mvvm.ComponentModel;
using ChessApp.Models;
using System.Collections.Generic;
using System;

namespace ChessApp.ViewModels;

public partial class MatchViewModel : ViewModelBase
{
    private readonly Match _matchModel;

    public MatchViewModel(Match matchModel)
    {
        _matchModel = matchModel;
        _result = matchModel.Result;
        _moves = string.Join("\n", matchModel.Moves);
    }

    public Player? FirstParticipant => _matchModel.WhitePlayer;
    public Player? SecondParticipant => _matchModel.BlackPlayer;

    [ObservableProperty] private string _result;
    [ObservableProperty] private string _moves;

    partial void OnResultChanged(string value) => _matchModel.Result = value;

    partial void OnMovesChanged(string value)
    {
        _matchModel.Moves = new List<string>(
            value.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
        );
    }
}