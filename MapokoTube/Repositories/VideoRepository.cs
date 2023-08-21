using System.Data;
using MapokoTube.Interfaces;
using MapokoTube.Models;
using MySql.Data.MySqlClient;

namespace MapokoTube.Repositories;

public class VideoRepository : IVideoRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=MapokoTubedb;uid=root;pwd=''";

    public void Create(Video model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Video(Name, Description, UploadDate, Duration, VideoFile, Thumbnail) "
              + "values (@Name, @Description, @UploadDate, @Duration, @VideoFile, @Thumbnail)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Description", model.Description);
        command.Parameters.AddWithValue("@UploadDate", model.UploadDate);
        command.Parameters.AddWithValue("@Duration", model.Duration);
        command.Parameters.AddWithValue("@VideoFile", model.VideoFile);
        command.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Video where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Video> ReadAll()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };

        List<Video> Videos = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video Video = new()
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                UploadDate = reader.GetDateTime("UploadDate"),
                Duration = reader.GetInt16("duration"),
                Thumbnail = reader.GetString("Thumbnail"),
                VideoFile = reader.GetString("VideoFile")
            };
            Videos.Add(Video);
        }
        connection.Close();
        return Videos;
    }

    public Video ReadById(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            Video Video = new()
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                UploadDate = reader.GetDateTime("UploadDate"),
                Duration = reader.GetInt16("duration"),
                Thumbnail = reader.GetString("Thumbnail"),
                VideoFile = reader.GetString("VideoFile")
            };
            connection.Close();
            return Video;
        }
        connection.Close();
        return null;
    }

    public void Update(Video model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "update Video   set "
                        + "Name = @Name, "
                        + "Description = @Description, "
                        + "UploadDate = @UploadDate, "
                        + "Duration = @Duration, "
                        + "VideoFile = @VideoFile, "
                        + "Thumbnail = @Thumbnail "
                    + "where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", model.Id);
        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Description", model.Description);
        command.Parameters.AddWithValue("@UploadDate", model.UploadDate);
        command.Parameters.AddWithValue("@Duration", model.Duration);
        command.Parameters.AddWithValue("@VideoFile", model.VideoFile);
        command.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}