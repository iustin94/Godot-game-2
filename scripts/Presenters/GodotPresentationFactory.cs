using DungeonKeeper.Core.Entities;
using DungeonKeeper.Core.Presentation;
using Godot;

namespace DungeonKeeper.Scripts.Presenters;

public class GodotPresentationFactory : IPresentationFactory
{
    private readonly Node3D _mapRoot;
    private readonly Node3D _creaturesRoot;
    private readonly Node3D _roomsRoot;
    private readonly Node3D _effectsRoot;

    private GodotMapPresenter? _mapPresenter;
    private GodotCombatPresenter? _combatPresenter;
    private GodotSpellPresenter? _spellPresenter;
    private GodotAudioPresenter? _audioPresenter;

    // Shared node map so combat presenter can find creature meshes
    private readonly Dictionary<EntityId, MeshInstance3D> _creatureNodes = new();

    // Cache presenters per entity to avoid creating duplicates
    private readonly Dictionary<EntityId, ICreaturePresenter> _creaturePresenters = new();
    private readonly Dictionary<EntityId, IRoomPresenter> _roomPresenters = new();

    public GodotPresentationFactory(Node3D mapRoot, Node3D creaturesRoot, Node3D roomsRoot, Node3D effectsRoot)
    {
        _mapRoot = mapRoot;
        _creaturesRoot = creaturesRoot;
        _roomsRoot = roomsRoot;
        _effectsRoot = effectsRoot;
    }

    public GodotMapPresenter MapPresenter => _mapPresenter ??= new GodotMapPresenter(_mapRoot);

    public ICreaturePresenter CreateCreaturePresenter(EntityId entityId, string assetId)
    {
        if (_creaturePresenters.TryGetValue(entityId, out var existing))
            return existing;

        var presenter = new GodotCreaturePresenter(_creaturesRoot, entityId, assetId, _creatureNodes);
        _creaturePresenters[entityId] = presenter;
        return presenter;
    }

    public IRoomPresenter CreateRoomPresenter(EntityId roomId, string assetId)
    {
        if (_roomPresenters.TryGetValue(roomId, out var existing))
            return existing;

        var presenter = new GodotRoomPresenter(_roomsRoot);
        _roomPresenters[roomId] = presenter;
        return presenter;
    }

    public IMapPresenter CreateMapPresenter()
    {
        return MapPresenter;
    }

    public ISpellPresenter CreateSpellPresenter()
    {
        return _spellPresenter ??= new GodotSpellPresenter(_effectsRoot);
    }

    public ICombatPresenter CreateCombatPresenter()
    {
        return _combatPresenter ??= new GodotCombatPresenter(_effectsRoot, _creatureNodes);
    }

    public ITrapPresenter CreateTrapPresenter(EntityId entityId, string assetId)
    {
        return new GodotTrapPresenter(_mapRoot);
    }

    public IAudioPresenter CreateAudioPresenter()
    {
        return _audioPresenter ??= new GodotAudioPresenter();
    }
}
