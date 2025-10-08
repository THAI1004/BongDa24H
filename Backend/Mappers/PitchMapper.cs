using Backend.Dtos.Pitch;
using Backend.Models;

namespace Backend.Mappers;

public static class PitchMapper
{
    public static PitchDto ToPitchDto(this Pitch pitch)
    {
        return new PitchDto
        {
            Id = pitch.Id,
            PitchName = pitch.PitchName,
            ClusterId = pitch.ClusterId,
            PitchType = pitch.PitchType,
            ImageUrl = pitch.ImageUrl,
            Facilities = pitch.Facilities
        };
    }
    public static Pitch ToPitchCreate(this CreatePitchDto createPitchDto)
    {
        return new Pitch
        {
            PitchName = createPitchDto.PitchName,
            ClusterId = createPitchDto.ClusterId,
            PitchType = createPitchDto.PitchType,
            ImageUrl = createPitchDto.ImageUrl,
            Facilities = createPitchDto.Facilities
        };
    }
}