using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OsuSpectateRandomBot
{
    public class Location
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("country_code")]
        public int CountryCode { get; set; }
    }

    public class Beatmap
    {
        [JsonPropertyName("md5")]
        public string Md5 { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Action
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("beatmap")]
        public Beatmap Beatmap { get; set; }

        [JsonPropertyName("mods")]
        public int Mods { get; set; }

        [JsonPropertyName("game_mode")]
        public int GameMode { get; set; }
    }

    public class Client
    {
        [JsonPropertyName("api_identifier")]
        public string ApiIdentifier { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("privileges")]
        public int Privileges { get; set; }

        [JsonPropertyName("silence_end_time")]
        public object SilenceEndTime { get; set; }

        [JsonPropertyName("action")]
        public Action Action { get; set; }

        [JsonPropertyName("bancho_privileges")]
        public int BanchoPrivileges { get; set; }
    }

    public class CRippleResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("clients")]
        public Dictionary<string, List<Client>> Users { get; set; }

        [JsonPropertyName("connected_users")]
        public int ConnectedUsers { get; set; }

        [JsonPropertyName("connected_clients")]
        public int ConnectedClients { get; set; }
    }

    public enum ActionType
    {
        Idle = 0,
        Afk = 1,
        Playing = 2,
        EditingBeatmap = 3,
        ModdingBeatmap = 4,
        InMultiplayerMatch = 5,
        Watching = 6,
        UNUSED = 7,
        TestingBeatmap = 8,
        SubmittingBeatmap = 9,
        Paused = 10,
        InMultiplayerLobby = 11,
        PlayingMultiplayerMatch = 12,
        UsingDirect = 13,
    }

    public enum ClientType
    {
        Game = 0,
        IRC,
        Fake,
        Web,
    }
}